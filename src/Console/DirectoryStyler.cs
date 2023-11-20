using ProjectScaffold.Common.Abstractions;
using ProjectScaffold.Constants;

namespace ProjectScaffold.Strategy;

internal sealed class DirectoryStyler : ITreeNodeStyler
{
    public string Icon => FileName switch
    {
        SpecificFolderNamings.Src => "",
        SpecificFolderNamings.Test => "\udb81\ude68",
        _ => "\uf07b"

    };

    public string FileName {get; set;} = null!;

    public string Color => FileName switch {
        SpecificFolderNamings.Src => "[green]",
        SpecificFolderNamings.Test => "[hotpink2]",
        _ => "[silver]"

    };

    public string FullStyle => $"{Color}{Icon} {FileName}[/]";

    public string Stylize(string directory, string file)
    {
        FileName = Path.GetRelativePath(directory,file);
        return FullStyle;
    }
}