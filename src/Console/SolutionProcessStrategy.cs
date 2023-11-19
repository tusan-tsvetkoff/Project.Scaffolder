using System.Diagnostics;
using Spectre.Console;

namespace ProjectScaffold.Strategy;

public class SolutionProcessStrategy : IProcessStrategy
{
    public bool IsFinished { get; private set; } = false;

    public async Task Execute(ProjectBase project, ProgressTask task)
    {
        var sol = (Solution)project;

        var processInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"new sln -o {sol.Name}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        try
        {
            using var process = new Process { StartInfo = processInfo };
            task.StartTask();
            AnsiConsole.MarkupLine($"Creating solution [u]{sol}[/]...");
            process.Start();

            while (!process!.HasExited)
            {
                task.Increment(17.0);
                await Task.Run(() => process.WaitForExit(100));
            }
            AnsiConsole.MarkupLine($"[green]Created [u]{sol}[/][/]!");
            task.StopTask();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An error occurred: {ex.Message}[/]");
        }
    }
}
