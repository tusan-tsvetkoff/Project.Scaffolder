using System.Reflection;

namespace ProjectScaffold;

public abstract class ProjectBase
{
    public virtual string Name { get; set; } = "MyProject";
    public virtual string Suffix => ".csproj";
    public virtual char Icon { get; } = '\ue70c'; // visual studio icon essentially
    public abstract string Directory { get; }
    public virtual string RelativePath => Name;

    public override string ToString()
    {
        return $"{Name}{Suffix}";
    }
}
