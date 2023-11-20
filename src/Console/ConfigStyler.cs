using ProjectScaffold.Common.Abstractions;

namespace ProjectScaffold.Strategy;

public sealed class ConfigStyler : ITreeNodeStyler
{
    public string Icon => DetermineIcon();
    private string Extension { get; set; } = null!;

    public string FileName { get; private set; } = null!;

    public string Color => "[red]";

    public string FullStyle => $"{Color}{Icon} {FileName}[/]";

    public string Stylize(string directory, string file)
    {
        FileName = Path.GetRelativePath(directory, file);
        Extension = Path.GetExtension(file);
        return FullStyle;
    }

    private string DetermineIcon()
    {
        return Extension switch
        {
            ".json" => "\ue60b",
            ".xml" => "\ue619",
            ".config" => "\ue619",
            ".props" => "\ue619",
            ".targets" => "\ue619",
            ".toml" => "\ue619",
            ".csv" => "\ue619",
            ".yaml" => "\ue619",
            ".yml" => "\ue619",
            ".proto" => "\ue619",
            ".hjson" => "\ue619",
            ".md" => "\ue619",
            _ => "\ue77f"
        };
    }
}
