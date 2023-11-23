using ProjectScaffold.Common.Abstractions;
using ProjectScaffold.Models;
using ProjectScaffold.Strategy;

namespace ProjectScaffold.Common;

public static class Determine
{
    /// <summary>
    /// Determines the <see cref="IProcessStrategy"/> to use, based on the given project type.
    /// </summary>
    /// <param name="project">The project type.</param>
    /// <returns>The <see cref="IProcessStrategy"/></returns>
    public static IProcessStrategy Strategy(ProjectBase project)
    {
        return project.GetType().Name switch
        {
            //! Currently only 3 possible project types, that's why we can use a default case with
            //! a ternary operator. If I add more project types, we'll need to change this.
            nameof(Solution)
                => new SolutionProcessStrategy(),
            _
                => project.GetType().Name == nameof(TestProject)
                    ? new TestProcessStrategy()
                    : new SourceProcessStrategy()
        };
    }

    /// <summary>
    /// Determines the name of the Spectre.Console progress task, based on the given project type.
    /// </summary>
    /// <param name="project">The project type.</param>
    /// <returns>The name of the progress task.</returns>
    public static string TaskName(ProjectBase project)
    {
        return project.GetType().Name switch
        {
            nameof(Solution) => $"Creating solution {project.Name}",
            nameof(TestProject) => $"Creating test project {project.Name}",
            _ => $"Creating project {project.Name}"
        };
    }
}
