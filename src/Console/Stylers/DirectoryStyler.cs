using ProjectScaffold.Common.Abstractions;
using ProjectScaffold.Constants;

namespace ProjectScaffold.Strategy;

internal sealed class DirectoryStyler : Styler, ITreeNodeStyler
{
    public override string Icon => FileName switch
    {
        SpecificFolderNamings.Src => "",
        SpecificFolderNamings.Test => "\udb81\ude68",
        _ => "\uf07b"

    };

    public override string FileName { get; protected set; } = null!;

    public override string Color => FileName switch
    {
        SpecificFolderNamings.Src => "[green]",
        SpecificFolderNamings.Test => "[hotpink2]",
        _ => "[silver]"

    };

    public string Stylize(string directory, string file)
    {
        FileName = Path.GetRelativePath(directory, file);
        return FullStyle;
    }
}