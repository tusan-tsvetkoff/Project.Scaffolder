namespace ProjectScaffold.Console;

public sealed class SourceDirectory
{
    private static readonly object lockObject = new();
    private static SourceDirectory? _instance;
    public static SourceDirectory Instance
    {
        get
        {
            if (_instance is null)
            {
                lock (lockObject)
                {
                    _instance ??= new SourceDirectory();
                }
            }
            return _instance;
        }
    }
    private readonly List<ProjectBase> _projects = new();
    public List<ProjectBase> Projects => _projects;

    private SourceDirectory() { }

    public void AddProject(ProjectBase project)
    {
        _projects.Add(project);
    }

    public void AddProjects(params ProjectBase[] projects)
    {
        foreach (var project in projects)
        {
            _projects.Add(project);
        }
    }

    public void AddProjects(IEnumerable<ProjectBase> projects)
    {
        foreach (var project in projects)
        {
            _projects.Add(project);
        }
    }
}
