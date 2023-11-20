using ProjectScaffold.Common.Abstractions;

namespace ProjectScaffold.Strategy;

public sealed class DefaultStyler : ITreeNodeStyler
{
    public string Icon => "\uf15b";
    public string FileName {get; private set;} = null!;
    public string Color => "[silver]";
    public string FullStyle => $"{Color}{Icon} {FileName}[/]";

    public string Stylize(string directory,string file)
    {
        FileName = Path.GetRelativePath(directory,file);
        return FullStyle;
    }
}