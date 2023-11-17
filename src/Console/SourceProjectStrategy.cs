using System.Diagnostics;
using Spectre.Console;

namespace ProjectScaffold.Console.Strategy;

public class SourceProcessStrategy : IProcessStrategy
{
    public bool IsFinished => false;

    public async Task Execute(ProjectBase project, ProgressTask task)
    {
        var srcProject = (SourceProject)project;

        var processInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"new {srcProject.TypeName} -n {srcProject.Name}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = srcProject.Directory
        };

        try
        {
            using var process = new Process { StartInfo = processInfo };
            task.StartTask();
            AnsiConsole.MarkupLine($"Creating [u]{srcProject}[/]...");
            process.Start();

            while (!process!.HasExited)
            {
                task.Increment(17.0);
                await Task.Run(() => process.WaitForExit(100));
            }
            AnsiConsole.MarkupLine($"[green]Created [u]{srcProject}[/][/]!");
            task.StopTask();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An error occurred: {ex.Message}[/]");
        }
    }
}
