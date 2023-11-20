using ProjectScaffold.Models;

namespace ProjectScaffold.Builders;

public class SolutionBuilder
{
    private bool _makeSrc = false;
    private bool _makeTest = false;
    private string _name = string.Empty;

    public SolutionBuilder() { }

    public SolutionBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public SolutionBuilder MakeSrc(bool makeSrc)
    {
        _makeSrc = makeSrc;
        return this;
    }

    public SolutionBuilder MakeTest(bool makeTest)
    {
        _makeTest = makeTest;
        return this;
    }

    public Solution Build()
    {
        var sol = Solution.Create(_makeSrc, _makeTest);
        sol.SetName(_name);

        return sol;
    }
}
