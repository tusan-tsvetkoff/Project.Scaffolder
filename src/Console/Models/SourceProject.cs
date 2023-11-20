using ProjectScaffold.Constants;
using ProjectScaffold.Enums;
using ProjectScaffold.Utils;

namespace ProjectScaffold.Models;

public sealed class SourceProject : ProjectBase
{
    public string ProjectName { get; private set; } = null!;
    public override string Name => $"{Solution.Name}.{ProjectName}";
    public Solution Solution { get; private set; } = null!;
    public ProjectType Type { get; private set; }
    public override string Directory => Path.Combine(Solution.Name, SpecificFolderNamings.Src);
    public override string RelativePath => Path.Combine(SpecificFolderNamings.Src, Name);
    public string TypeName =>
        Type switch
        {
            ProjectType.Console => DotNetCommands.Console,
            ProjectType.Library => DotNetCommands.ClassLib,
            ProjectType.AspNetCoreEmpty => DotNetCommands.AspNetCoreEmpty,
            ProjectType.Mvc => DotNetCommands.Mvc,
            ProjectType.AspNetCoreWebApi => DotNetCommands.AspNetCoreWebApi,
            _ => throw new NotImplementedException()
        };

    private SourceProject(string projectName, Solution solution, ProjectType type)
    {
        ProjectName = projectName;
        Solution = solution;
        Type = type;
    }

    public static IEnumerable<SourceProject> CreateMany(
        IEnumerable<string> projectNames,
        Solution solution,
        IEnumerable<ProjectType> types
    )
    {
        Ensure.EqualCount(projectNames, types);

        var projects = projectNames.Zip(types, (projectName, type) => (projectName, type));

        foreach (var (projectName, type) in projects)
        {
            yield return new SourceProject(projectName, solution, type);
        }
    }
}
