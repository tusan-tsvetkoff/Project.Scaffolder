using ProjectScaffold.Strategy;
using ProjectScaffold.Builders;
using ProjectScaffold.View;

namespace ProjectScaffold.Main;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await RunProjectScaffolder();
    }

    internal static async Task RunProjectScaffolder()
    {
        var solutionBuilder = new SolutionBuilder();
        var directoryBuilder = new DirectoryBuilder();
        var testBuilder = new TestBuilder();
        var projectMaker = new ProjectMaker();

        var gui = new Gui(solutionBuilder, directoryBuilder, testBuilder, projectMaker);
        await gui.Run();
    }
}
