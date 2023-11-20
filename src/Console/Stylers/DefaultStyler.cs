using ProjectScaffold.Common.Abstractions;

namespace ProjectScaffold.Strategy;

public sealed class DefaultStyler : Styler, ITreeNodeStyler
{
    public override string FileName { get; protected set; } = null!;
    public override string Color => "[silver]";

    public string Stylize(string directory, string file)
    {
        FileName = Path.GetRelativePath(directory, file);
        return FullStyle;
    }
}