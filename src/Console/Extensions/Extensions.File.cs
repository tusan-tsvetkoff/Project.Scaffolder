namespace ProjectScaffoldj.Extensions;

public static partial class Extensions{
    public static bool IsDirectory(this string path)
    {
        return Directory.Exists(path);
    }
}