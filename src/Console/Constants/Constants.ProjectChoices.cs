namespace ProjectScaffold.Constants;

internal static partial class Constants
{
    public static class ProjectChoices
    {
        public const string Console = "Console";
        public const string Library = "Library";
        public const string AspNetCoreEmpty = "ASP.NET Core Empty";
        public const string AspNetCoreWebAppMvc = "ASP.NET Core Web App (Model-View-Controller)";
        public const string AspNetCoreWebApi = "ASP.NET Core Web API";

        public static readonly List<string> All =
        [
            Console,
            Library,
            AspNetCoreEmpty,
            AspNetCoreWebAppMvc,
            AspNetCoreWebApi
        ];
    }
}
