using ProjectScaffold.Common.Abstractions;

namespace ProjectScaffold.Strategy;

public sealed class ConfigStyler : ITreeNodeStyler
{
    public string Icon => DetermineIcon(Extension);
    private string Extension { get; set; } = null!;

    public string FileName => throw new NotImplementedException();

    public string Color => throw new NotImplementedException();

    public string FullStyle => throw new NotImplementedException();

    public string Stylize(string directory, string file)
    {
        throw new NotImplementedException();
    }

    private string DetermineIcon(string extension)
    {
        return extension switch
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
