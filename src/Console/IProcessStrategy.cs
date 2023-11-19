using Spectre.Console;

namespace ProjectScaffold.Strategy;

public interface IProcessStrategy
{
    bool IsFinished { get; }
    Task Execute(ProjectBase project, ProgressTask task);
}
