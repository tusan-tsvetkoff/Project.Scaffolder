using Spectre.Console;

namespace ProjectScaffold.Models;

public sealed class SourceDirectory : DirectoryBase // TODO: This is shite
{
    private SourceDirectory(Solution solution)
        : base(solution) { }

    public override string Name => "src";
    public override char Icon => '\uf209'; // src folder icon

    public static SourceDirectory? CreateDirectory(Solution solution)
    {
        if (!solution.MakeTest)
        {
            return null;
        }
        var dir = new SourceDirectory(solution);
        var created = dir.MakeDirectory();
        AnsiConsole.MarkupLine($"[green]Created directory [u]{created.FullName}[/][/]");
        return dir;
    }
}
