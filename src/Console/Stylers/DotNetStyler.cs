using ProjectScaffold.Common.Abstractions;
using ProjectScaffold.Utils;

namespace ProjectScaffold.Strategy;

public sealed class DotNetStyler : Styler, ITreeNodeStyler
{
    public override string Icon => IconMappingService.Instance.GetIcon<DotNetStyler>(Extension);

    public override string FileName { get; protected set; } = null!;

    public string Extension { get; private set; } = null!;

    public override string Color => Extension switch
    {
        ".cs" => "[blue]",
        ".csproj" => "[purple_1]",
        ".sln" => "[darkmagenta]",
        _ => "[blue]"
    };


    public string Stylize(string directory, string fileName)
    {
        FileName = Path.GetRelativePath(directory, fileName);
        Extension = Path.GetExtension(fileName);
        return FullStyle;
    }
}
