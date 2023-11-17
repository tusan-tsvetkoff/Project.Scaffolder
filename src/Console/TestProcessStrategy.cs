using System.Diagnostics;
using Spectre.Console;

namespace ProjectScaffold.Console.Strategy;

public class TestProcessStrategy : IProcessStrategy
{
    public bool IsFinished { get; private set; } = false;

    public async Task Execute(ProjectBase project, ProgressTask task)
    {
        var test = (Test)project;

        var processInfo = new ProcessStartInfo()
        {
            FileName = "dotnet",
            Arguments = $"new {test.FrameWorkName} -n {test.Name}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = test.Directory
        };

        try
        {
            using var process = new Process { StartInfo = processInfo };

            task.StartTask();
            AnsiConsole.MarkupLine($"Creating [u]{test}[/]...");
            process.Start();

            while (!process!.HasExited)
            {
                task.Increment(4.8);
                await Task.Run(() => process.WaitForExit(100));
            }
            AnsiConsole.MarkupLine($"[green]Created [u]{test}[/][/]!");
            task.StopTask();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An error occurred: {ex.Message}[/]");
        }
    }
}
