using ProjectScaffold.Models;

namespace ProjectScaffold.Builders;

public sealed class DirectoryBuilder
{
    private Solution _solution = null!;

    public void SetSolution(Solution solution)
    {
        _solution = solution;
    }

    public (SourceDirectory srcDir, TestDirectory? testDir) Build()
    {
        var src = SourceDirectory.CreateDirectory(_solution);
        var test = TestDirectory.CreateDirectory(_solution);
        return (src!, test);
    }
}
