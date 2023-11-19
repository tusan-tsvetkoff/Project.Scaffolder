namespace ProjectScaffold.Constants;

internal static partial class Constants
{
    public static class TestChoices
    {
        public const string XUnit = "xUnit";
        public const string NUnit = "NUnit";
        public const string MSTest = "MSTest";

        public static readonly IEnumerable<string> All = new List<string> { XUnit, NUnit, MSTest };
    }
}
