namespace ProjectScaffold.Strategy;

public abstract class Styler
{
    public virtual string Icon => "\uf15b";
    public abstract string FileName { get; protected set; }
    public abstract string Color { get; }
    public string FullStyle => $"{Color}{Icon} {FileName}[/]";
}
