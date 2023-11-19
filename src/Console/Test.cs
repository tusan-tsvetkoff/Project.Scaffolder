using ProjectScaffold.Enums;

namespace ProjectScaffold;

internal sealed class Test : ProjectBase
{
    public override string Name => $"{Solution.Name}.{TypeName}.Tests";
    public TestFramework FrameWork { get; set; } = TestFramework.XUnit;
    public TestType Type { get; set; } = TestType.Unit;
    public Solution Solution { get; set; } = null!;
    public override string Directory => Path.Combine(Solution.Name, "test");
    public override string RelativePath => Path.Combine("test", Name);
    public string FrameWorkName =>
        FrameWork switch
        {
            TestFramework.XUnit => "xunit",
            TestFramework.NUnit => "nunit",
            _ => throw new NotImplementedException()
        };

    private string TypeName =>
        Type switch
        {
            TestType.Unit => "Unit",
            TestType.Integration => "Integration",
            TestType.Functional => "Functional",
            _ => throw new NotImplementedException()
        };
}
