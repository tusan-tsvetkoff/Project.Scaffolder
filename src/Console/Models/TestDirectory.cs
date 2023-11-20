using ProjectScaffold.Constants;
using Spectre.Console;

namespace ProjectScaffold.Models;

public sealed class TestDirectory : DirectoryBase
{
    private TestDirectory(Solution solution)
        : base(solution) { }

    public override string Name => SpecificFolderNamings.Test;
    public static new string Icon => "\udb81\ude68"; // test tube icon

    public static TestDirectory? CreateDirectory(Solution solution)
    {
        // TODO: Look into this
        if (!solution.MakeTest)
        {
            return null;
        }

        var dir = new TestDirectory(solution);
        var created = dir.MakeDirectory();

        AnsiConsole.MarkupLine($"[green]Created directory [u]{created.FullName}[/][/]");
        return dir;
    }
}
