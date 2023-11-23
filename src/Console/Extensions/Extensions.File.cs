namespace ProjectScaffoldj.Extensions;

public static partial class Extensions
{
    public static bool IsDirectory(this string path)
    {
        // TODO: method should be improved, feels hardcoded
        return Directory.Exists(path);
    }
}
