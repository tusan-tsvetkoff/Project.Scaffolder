using ProjectScaffold.Common.Abstractions;

namespace ProjectScaffold.Strategy;

public sealed class DotNetStyler : ITreeNodeStyler
{
    public string Icon => Extension switch
    {
        ".cs" => "\udb80\udf1b",
        ".csproj" => "\ue70c",
        ".sln" => "\ue70c",
        _ => "\ue77f"
    };

    public string FileName { get; private set; } = null!;

    public string Extension { get; private set; } = null!;

    public string Color => Extension switch
    {
        ".cs" => "[blue]",
        ".csproj" => "[purple_1]",
        ".sln" => "[darkmagenta]",
        _ => "[blue]"
    };

    public string FullStyle => $"{Color}{Icon} {FileName}[/]";

    public string Stylize(string directory, string fileName)
    {
        FileName = Path.GetRelativePath(directory, fileName);
        Extension = Path.GetExtension(fileName);
        return FullStyle;
    }
}
