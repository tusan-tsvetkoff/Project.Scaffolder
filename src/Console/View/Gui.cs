using System.Globalization;
using ProjectScaffold.Builders;
using ProjectScaffold.Common;
using ProjectScaffold.Enums;
using ProjectScaffold.Models;
using ProjectScaffold.Strategy;
using Spectre.Console;

namespace ProjectScaffold.View;

internal sealed class Gui(
    SolutionBuilder solutionBuilder,
    DirectoryBuilder directoryBuilder,
    TestBuilder testBuilder,
    ProjectMaker maker)
{
    private (SourceDirectory srcDir, TestDirectory? testDir) _directoryTuple;
    private readonly MainDirectory sourceDir = MainDirectory.Instance;
    private int LongestFileNameLength;
    private readonly TreeNodeStyler styler = new();
    private Solution solution = null!;
    private Tree _treeRoot = null!;
    private TestProject? test;
    private List<string> projectChoices = [];
    private List<ProjectType> chosenProjectTypes = [];
    private readonly List<string> projectNameList = [];
    private static readonly IEnumerable<string> projectChoiceList = Constants.Constants.ProjectChoices.All;
    private static readonly IEnumerable<string> testFrameworkChoiceList = Constants.Constants.TestChoices.All;
    private static readonly Dictionary<string, ProjectType> projectTypesDict =
        new()
        {
            // TODO: Figure out why I gotta type Constants.Constants
            { Constants.Constants.ProjectChoices.Console, ProjectType.Console },
            { Constants.Constants.ProjectChoices.Library, ProjectType.Library },
            { Constants.Constants.ProjectChoices.AspNetCoreEmpty, ProjectType.AspNetCoreEmpty },
            { Constants.Constants.ProjectChoices.AspNetCoreWebAppMvc, ProjectType.Mvc },
            { Constants.Constants.ProjectChoices.AspNetCoreWebApi, ProjectType.AspNetCoreWebApi }
        };

    public async Task Run()
    {
        WriteWelcomeMessage();

        solution = BuildSolution();

        projectChoices = ListAndSelectProjectTypes();
        chosenProjectTypes = projectChoices.Select(choice => projectTypesDict[choice]).ToList();
        ListAndEnterProjectNames();

        if (solution.MakeTest)
        {
            BuildTestProject();
        }

        sourceDir.AddProjects(SourceProject.CreateMany(projectNameList, solution, chosenProjectTypes));
        sourceDir.AddProject(solution);

        directoryBuilder.SetSolution(solution);
        _directoryTuple = directoryBuilder.Build();

        await MakeProjectsAsync();

        await solution.BuildSolution(sourceDir.Projects.Where(proj => proj is not Solution));

        ScanAndPrepareTree(solution.Directory, _treeRoot);
        var panel = new Panel(_treeRoot)
        {
            Header = new PanelHeader("Project Structure"),
            // TODO: Figure out why padding right is not working at all
        };

        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Builds a solution based on user input.
    /// </summary>
    /// <returns>The built solution.</returns>
    private Solution BuildSolution()
    {
        var sol = solutionBuilder
            .WithName(AnsiConsole.Ask("What is your [green]solution's name[/]?", "MyProject"))
            .MakeSrc(AnsiConsole.Confirm("Do you want to make a [green]src[/] directory?"))
            .MakeTest(AnsiConsole.Confirm("Do you want to make a [fuchsia]test[/] directory?"))
            .Build();

        _treeRoot = new Tree($"{sol.Icon} {sol.Name}"); // TODO: Change to folder icon 

        return sol;
    }


    /// <summary>
    /// Builds a test project by configuring the test builder with the solution, test framework, and test type.
    /// </summary>
    private void BuildTestProject()
    {
        testBuilder.WithSolution(solution);
        testBuilder.WithFramework(AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a [green]test framework[/]")
                .AddChoices(
                    Constants.Constants.TestChoices.All
                )
                    ) switch
        {
            Constants.Constants.TestChoices.XUnit => TestFramework.XUnit,
            Constants.Constants.TestChoices.NUnit => TestFramework.NUnit,
            Constants.Constants.TestChoices.MSTest => TestFramework.MSTest,
            _ => throw new Exception("Invalid test framework choice")
        });

        testBuilder.WithType(AnsiConsole.Prompt(
            new SelectionPrompt<TestType>()
                .Title("Select a [green]test type[/]")
                .AddChoices([TestType.Unit, TestType.Integration, TestType.Functional])
        ));

        test = testBuilder.Build();

        sourceDir.AddProject(test);
    }

    /// <summary>
    /// Asynchronously makes projects.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task MakeProjectsAsync() => await AnsiConsole
            .Progress()
            .Columns(
                [
                    new TaskDescriptionColumn(),
                    new ProgressBarColumn(),
                    new PercentageColumn(),
                    new RemainingTimeColumn(),
                    new SpinnerColumn(),
                ]
    )
    .StartAsync(async ctx =>
    {
        await Task.WhenAll(
            sourceDir.Projects.Select(async proj =>
            {
                var task = ctx.AddTask(
                    Determine.TaskName(proj),
                    new ProgressTaskSettings { MaxValue = 100, AutoStart = false }
                );

                maker.SetStrategy(Determine.Strategy(proj));

                await maker.Make(proj, task);
            })
        );
    });

    /// <summary>
    /// Writes the welcome message to the console.
    /// </summary>
    private static void WriteWelcomeMessage()
    {
        System.Console.OutputEncoding = System.Text.Encoding.UTF8; // Required for some glyphs to display correctly
        AnsiConsole.Write(new FigletText("Project Scaffold").Color(Color.Green));
        AnsiConsole.MarkupLine("Welcome to [green]Project Scaffold[/]!");
        AnsiConsole.MarkupLine(
            "This tool will help you create a new project based on the [green]Project Scaffold[/] template."
        );
    }

    /// <summary>
    /// Displays a list of project types and allows the user to select multiple types.
    /// </summary>
    /// <returns>A list of selected project types.</returns>
    private static List<string> ListAndSelectProjectTypes()
    {
        return AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("Select a [green]project type[/]")
                .InstructionsText("Press [green]<space>[/] to toggle, " + "[green]<enter>[/] to accept.")
                .AddChoices(
                    projectChoiceList
                )
        );
    }

    /// <summary>
    /// Lists the project types and prompts the user to enter the names for each project.
    /// </summary>
    private void ListAndEnterProjectNames()
    {
        foreach (var project in chosenProjectTypes)
        {
            var projectName = AnsiConsole.Ask(
                $"What is your [green][bold]{project}[/] project's name[/]?",
                $"[italic]{solution.Name}.{project})[/]"
            );
            projectNameList.Add(projectName);
        }
    }

    private void ScanAndPrepareTree(string directory, Tree parentNode, int depth = 0)
    {
        var files = Directory.GetFiles(directory);

        foreach (var file in files)
        {
            LongestFileNameLength = Math.Max(LongestFileNameLength, Path.GetRelativePath(directory, file).Length);
            if (depth >= 4)
            {
                continue;
            }

            styler.SetStyler(file);
            parentNode.AddNode(styler.Stylize(directory, file));
        }

        var subDirs = Directory.EnumerateDirectories(directory, "*", SearchOption.TopDirectoryOnly)
                            .Where(subDir => !subDir.EndsWith("obj") && !subDir.EndsWith("bin"));

        foreach (var subDir in subDirs)
        {
            LongestFileNameLength = Math.Max(LongestFileNameLength, Path.GetRelativePath(directory, subDir).Length);
            if (depth >= 4) // ! Hardcoded for now, but it essentially prevents the tree from getting too big
            {
                continue;
            }
            styler.SetStyler(subDir);
            var subDirNode = new Tree(styler.Stylize(directory, subDir));
            parentNode.AddNode(subDirNode);
            ScanAndPrepareTree(subDir, subDirNode, depth + 1);
        }
    }
}
