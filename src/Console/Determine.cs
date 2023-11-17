using ProjectScaffold.Console;
using ProjectScaffold.Console.Strategy;

namespace ProjectScaffold.Common;

public static class Determine
{
    public static IProcessStrategy Strategy(ProjectBase project)
    {
        return project.GetType().Name switch
        {
            nameof(Solution) => new SolutionProcessStrategy(),
            _
                => project.GetType().Name == nameof(Test)
                    ? new TestProcessStrategy()
                    : new SourceProcessStrategy()
        };
    }

    public static string TaskName(ProjectBase project)
    {
        return project.GetType().Name switch
        {
            nameof(Solution) => $"Creating solution {project.Name}",
            nameof(Test) => $"Creating test project {project.Name}",
            _ => $"Creating project {project.Name}"
        };
    }
}
