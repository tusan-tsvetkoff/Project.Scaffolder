using Spectre.Console;

namespace ProjectScaffold.Console.Strategy;

public class ProjectMaker
{
    private IProcessStrategy _processStrategy = null!;
    public bool IsFinished => _processStrategy.IsFinished;

    public ProjectMaker() { }

    public void SetStrategy(IProcessStrategy processStrategy)
    {
        _processStrategy = processStrategy;
    }

    public async Task Make(ProjectBase project, ProgressTask task)
    {
        await _processStrategy.Execute(project, task);
    }
}
