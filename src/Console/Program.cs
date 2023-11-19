using ProjectScaffold.Strategy;
using ProjectScaffold.Builders;
using ProjectScaffold.View;

var solutionBuilder = new SolutionBuilder();
var directoryBuilder = new DirectoryBuilder();
var testBuilder = new TestBuilder();
var projectMaker = new ProjectMaker();

var gui = new Gui(solutionBuilder, directoryBuilder, testBuilder, projectMaker);
await gui.Run();
