using Spectre.Console;

namespace ProjectScaffold.Models;

public abstract class DirectoryBase
{
    public abstract string Name { get; }
    public virtual char Icon { get; } = '\uf07b'; // folder icon
    public Solution Solution { get; set; } = null!;

    protected DirectoryBase(Solution solution)
    {
        Solution = solution;
    }

    protected DirectoryInfo MakeDirectory()
    {
        return Directory.CreateDirectory(Path.Combine(Solution.Name, Name));
    }
}

public sealed class TestDirectory : DirectoryBase
{
    private TestDirectory(Solution solution)
        : base(solution) { }

    public override string Name => "test";
    public static new string Icon => "\udb81\ude68"; // test tube icon

    public static TestDirectory? CreateDirectory(Solution solution)
    {
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

public sealed class SSourceDirectory : DirectoryBase // TODO: This is shite
{
    private SSourceDirectory(Solution solution)
        : base(solution) { }

    public override string Name => "src";
    public override char Icon => '\uf209'; // src folder icon

    public static SSourceDirectory? CreateDirectory(Solution solution)
    {
        if (!solution.MakeTest)
        {
            return null;
        }
        var dir = new SSourceDirectory(solution);
        var created = dir.MakeDirectory();
        AnsiConsole.MarkupLine($"[green]Created directory [u]{created.FullName}[/][/]");
        return dir;
    }
}
