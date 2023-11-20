using ProjectScaffold.Constants;

namespace ProjectScaffold.Utils;

public sealed partial class IconMappingService
{
    private static class IconMappings
    {
        internal static readonly Dictionary<string, string> ConfigMappings = new()
        {
            {".json", "\ue60b"},
            {".xml", "\ue619"},
            {".config", "\ue619"},
            {".props", "\ue619"},
            {".targets", "\ue619"},
            {".toml", "\ue619"},
            {".csv", "\ue619"},
            {".yaml", "\ue619"},
            {".yml", "\ue619"},
            {".proto", "\ue619"},
            {".hjson", "\ue619"},
            {".md", "\ue619"},
            {"default", "\ue77f"}
        };

        internal static readonly Dictionary<string, string> DotNetMappings = new()
        {
            {".cs", "\udb80\udf1b"},
            {".csproj", "\ue70c"},
            {".sln", "\ue70c"},
            {"default", "\ue77f"}
        };

        internal static readonly Dictionary<string, string> DirectoryMappings = new()
        {
            {SpecificFolderNamings.Src , ""},
            {SpecificFolderNamings.Test , "\udb81\ude68"},
            {"default", "\uf07b"}
        };
    }
}