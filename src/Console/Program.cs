using ProjectScaffold.Strategy;
using ProjectScaffold.Builders;
using ProjectScaffold.View;
using FileExplorer.Main;

// var solutionBuilder = new SolutionBuilder();
// var directoryBuilder = new DirectoryBuilder();
// var testBuilder = new TestBuilder();
// var projectMaker = new ProjectMaker();

// var gui = new Gui(solutionBuilder, directoryBuilder, testBuilder, projectMaker);
// await gui.Run();


var fe = new FileTreeExplorer(@"C:\");
fe.Run();
