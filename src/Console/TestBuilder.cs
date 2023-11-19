using ProjectScaffold.Enums;
using ProjectScaffold.Models;

namespace ProjectScaffold.Builders;

public sealed class TestBuilder
{
    private Solution _solution = null!;
    private TestFramework _framework = TestFramework.XUnit; //! STUPID
    private TestType _type = TestType.Unit; //! STUPID

    public TestBuilder WithSolution(Solution solution)
    {
        _solution = solution;
        return this;
    }

    internal TestBuilder WithFramework(TestFramework framework)
    {
        _framework = framework;
        return this;
    }

    internal TestBuilder WithType(TestType type)
    {
        _type = type;
        return this;
    }

    internal Test Build()
    {
        return new Test
        {
            Solution = _solution,
            FrameWork = _framework,
            Type = _type
        };
    }
}
