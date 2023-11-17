using System.Diagnostics;
using Spectre.Console;

namespace ProjectScaffold;

public sealed class Solution : ProjectBase
{
    public bool MakeSrc { get; set; }
    public bool MakeTest { get; set; }
    public override string Suffix => ".sln";
    public override string Directory => Name;

    public async Task BuildSolution(IEnumerable<ProjectBase> projects)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"sln add {string.Join(" ", projects.Select(p => p.RelativePath))}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = Directory //? hopefully works
        };

        try
        {
            AnsiConsole.MarkupLine($"The path I am trying to add: {startInfo.Arguments}");

            using var process = Process.Start(startInfo);

            await Task.Run(() => process!.WaitForExit(100));
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]An error occurred: {ex.Message}[/]");
            Environment.Exit(1);
        }
    }
}
