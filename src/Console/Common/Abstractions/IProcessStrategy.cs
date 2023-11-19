using ProjectScaffold.Models;
using Spectre.Console;

namespace ProjectScaffold.Common.Abstractions;

public interface IProcessStrategy
{
    bool IsFinished { get; }
    Task Execute(ProjectBase project, ProgressTask task);
}
