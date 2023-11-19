using System.Diagnostics;
using Spectre.Console;

namespace ProjectScaffold.Models;

public sealed class Solution : ProjectBase
{
    public bool MakeSrc { get; private set; }
    public bool MakeTest { get; private set; }
    public override string Suffix => ".sln";
    public override string Directory => Name;

    private Solution(bool makeSrc, bool makeTest)
    {
        MakeSrc = makeSrc;
        MakeTest = makeTest;
    }

    public static Solution Create(bool makeSrc, bool makeTest)
    {
        return new Solution(makeSrc, makeTest);
    }

    public void SetName(string name)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        Name = name.Equals(string.Empty) ? Name : name;
    }

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
