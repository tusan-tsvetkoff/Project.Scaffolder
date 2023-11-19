using ProjectScaffold;
using ProjectScaffold.Common;
using ProjectScaffold.Console;
using ProjectScaffold.Strategy;
using ProjectScaffold.Constants;
using Spectre.Console;
using ProjectScaffold.Enums;

var sourceDir = SourceDirectory.Instance;
var solution = new Solution();
var maker = new ProjectMaker();
var test = new Test();
Console.OutputEncoding = System.Text.Encoding.UTF8;

var solName = AnsiConsole.Ask("What is your [green]solution's name[/]?", "MyProject");

solution.MakeSrc = AnsiConsole.Confirm("Do you want to make a [green]src[/] directory?");
solution.MakeTest = AnsiConsole.Confirm("Do you want to make a [fuchsia]test[/] directory?");

var projectChoice = AnsiConsole.Prompt(
    new MultiSelectionPrompt<string>()
        .Title("Select a [green]project type[/]")
        .InstructionsText("Press [green]<space>[/] to toggle, " + "[green]<enter>[/] to accept.")
        .AddChoices(
            new[]
            {
                Constants.ProjectChoices.Console,
                Constants.ProjectChoices.Library,
                Constants.ProjectChoices.AspNetCoreEmpty,
                Constants.ProjectChoices.AspNetCoreWebAppMvc,
                Constants.ProjectChoices.AspNetCoreWebApi
            }
        )
);

var projectTypeDict = new Dictionary<string, ProjectType>
{
    { Constants.ProjectChoices.Console, ProjectType.Console },
    { Constants.ProjectChoices.Library, ProjectType.Library },
    { Constants.ProjectChoices.AspNetCoreEmpty, ProjectType.AspNetCoreEmpty },
    { Constants.ProjectChoices.AspNetCoreWebAppMvc, ProjectType.Mvc },
    { Constants.ProjectChoices.AspNetCoreWebApi, ProjectType.AspNetCoreWebApi }
};

var chosenProjects = projectChoice.Select(choice => projectTypeDict[choice]).ToList();

// Choose name for each project
var projectNameList = new List<string>();
foreach (var project in chosenProjects)
{
    var projectName = AnsiConsole.Ask<string>(
        $"What is your [green][bold]{project}[/] project's name[/]?",
        $"[italic]{solution.Name}.{project})[/]"
    );
    projectNameList.Add(projectName);
}

// Create source projects
sourceDir.AddProjects(SourceProject.CreateMany(projectNameList, solution, chosenProjects));

// TODO: Figure out a way to add multiple test projects

var frameworkChoice = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Select a [green]test framework[/]")
        .AddChoices(
            new[]
            {
                Constants.TestChoices.XUnit,
                Constants.TestChoices.NUnit,
                Constants.TestChoices.MSTest
            }
        )
);

test.Solution = solution;

test.FrameWork = frameworkChoice switch
{
    Constants.TestChoices.NUnit => TestFramework.NUnit,
    Constants.TestChoices.XUnit => TestFramework.XUnit,
    Constants.TestChoices.MSTest => TestFramework.MSTest,
    _ => throw new NotImplementedException()
};

test.Type = AnsiConsole.Prompt(
    new SelectionPrompt<TestType>()
        .Title("Select a [green]test type[/]")
        .AddChoices(new[] { TestType.Unit, TestType.Integration, TestType.Functional })
);

sourceDir.AddProjects(test, solution);

// TODO: Think of a better way
var srcDir = solution.MakeSrc ? SSourceDirectory.CreateDirectory(solution) : null;
var testDir = solution.MakeTest ? TestDirectory.CreateDirectory(solution) : null;

await AnsiConsole
    .Progress()
    .Columns(
        new ProgressColumn[]
        {
            new TaskDescriptionColumn(),
            new ProgressBarColumn(),
            new PercentageColumn(),
            new RemainingTimeColumn(),
            new SpinnerColumn(),
        }
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

await solution.BuildSolution(sourceDir.Projects.Where(proj => proj is not Solution));

var root = new Tree($"\uf07b {solution.Name}");
var sol = root.AddNode($"[purple4]{solution.Icon} {solution}[/]");
var srcNode = srcDir is not null ? root.AddNode($"[green]{srcDir.Icon} {srcDir.Name}[/]") : null;
var testNode = testDir is not null
    ? root.AddNode($"[fuchsia]{TestDirectory.Icon} {test.Name}[/]")
    : null;
var srcChildNodes = sourceDir.Projects.Where(proj => proj is SourceProject).ToList();
var testChildNodes = sourceDir.Projects.Where(proj => proj is Test).ToList();

srcChildNodes.ForEach(proj => srcNode?.AddNode($"[purple4_1]{proj.Icon} {proj}[/]"));
testChildNodes.ForEach(proj => testNode?.AddNode($"[fuchsia]{proj.Icon} {proj}[/]"));

AnsiConsole.Write(root);
