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

    public override string ToString()
    {
        return $"{Icon} {Name}";
    }
}
