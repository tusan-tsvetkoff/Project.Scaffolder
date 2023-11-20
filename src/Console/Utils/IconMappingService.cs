using ProjectScaffold.Common.Abstractions;
using ProjectScaffold.Strategy;

namespace ProjectScaffold.Utils;

public sealed partial class IconMappingService
{
    private static readonly Lazy<IconMappingService> lazy = new(() => new IconMappingService());

    public static IconMappingService Instance
    {
        get { return lazy.Value; }
    }

    private readonly Dictionary<Type, Dictionary<string, string>> _stylerIconMappings = [];

    private IconMappingService()
    {
        _stylerIconMappings[typeof(ConfigStyler)] = IconMappings.ConfigMappings;
        _stylerIconMappings[typeof(DotNetStyler)] = IconMappings.DotNetMappings;
        _stylerIconMappings[typeof(DirectoryStyler)] = IconMappings.DirectoryMappings;
    }

    /// <summary>
    /// Retrieves the icon associated with the specified extension for the given type of tree node styler.
    /// </summary>
    /// <typeparam name="T">The type of tree node styler.</typeparam>
    /// <param name="extension">The file extension.</param>
    /// <returns>The icon associated with the extension, or the default icon if no mapping is found.</returns>
    /// <remarks>
    /// This method looks up the icon mapping for the specified type of tree node styler and extension.
    /// If a mapping is found, it returns the associated icon. Otherwise, it returns the default icon.
    /// </remarks>
    internal string GetIcon<T>(string extension)
        where T : Styler, ITreeNodeStyler
    {
        return _stylerIconMappings[typeof(T)].TryGetValue(extension, out string? value)
        ? value
        : _stylerIconMappings[typeof(T)]["default"];
    }
}