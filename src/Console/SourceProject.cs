using ProjectScaffold.Enums;

namespace ProjectScaffold;

public sealed class SourceProject : ProjectBase
{
    public string ProjectName { get; private set; } = null!;
    public override string Name => $"{Solution.Name}.{ProjectName}";
    public Solution Solution { get; private set; } = null!;
    public ProjectType Type { get; private set; }
    public override string Directory => Path.Combine(Solution.Name, "src");
    public override string RelativePath => Path.Combine("src", Name);
    public string TypeName =>
        Type switch
        {
            ProjectType.Console => "console",
            ProjectType.Library => "classlib",
            ProjectType.AspNetCoreEmpty => "web",
            ProjectType.Mvc => "mvc",
            ProjectType.AspNetCoreWebApi => "webapi",
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
        EnsureEqualCount(projectNames, types);

        var projects = projectNames.Zip(types, (projectName, type) => (projectName, type));

        foreach (var (projectName, type) in projects)
        {
            yield return new SourceProject(projectName, solution, type);
        }
    }

    private static void EnsureEqualCount(
        IEnumerable<string> projectNames,
        IEnumerable<ProjectType> types
    )
    {
        if (projectNames.Count() != types.Count())
        {
            throw new ArgumentException("The number of project names and types must be equal.");
        }
    }
}
