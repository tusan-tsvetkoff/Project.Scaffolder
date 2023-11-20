using Console.Constants;
using ProjectScaffold.Common.Abstractions;
using ProjectScaffoldj.Extensions;

namespace ProjectScaffold.Strategy;

public sealed class TreeNodeStyler
{
    private ITreeNodeStyler _styler = null!;

    internal void SetStyler(string file)
    {
        if (file.IsDirectory())
        {
            _styler = new DirectoryStyler();
        }
        else
        {
            _styler = Path.GetExtension(file) switch
            {
                var extension when ExtensionCollections.DotNetExtensions.Contains(extension)
                    => new DotNetStyler(),
                var extension when ExtensionCollections.ConfigExtensions.Contains(extension)
                    => new ConfigStyler(),
                _ => new DefaultStyler()
            };
        }
    }

    public string Stylize(string directory, string file)
    {
        return _styler.Stylize(directory, file);
    }
}
