using ProjectScaffold.Common.Abstractions;
using ProjectScaffold.Utils;

namespace ProjectScaffold.Strategy;

public sealed class ConfigStyler : Styler, ITreeNodeStyler
{
    public override string Icon => IconMappingService.Instance.GetIcon<ConfigStyler>(Extension);
    public string Extension { get; private set; } = null!;

    public override string FileName { get; protected set; } = null!;

    public override string Color => "[red]";

    public string Stylize(string directory, string file)
    {
        FileName = Path.GetRelativePath(directory, file);
        Extension = Path.GetExtension(file);
        return FullStyle;
    }
}
