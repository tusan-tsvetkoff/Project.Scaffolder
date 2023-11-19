namespace ProjectScaffold.Models;

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

    /// <summary>
    /// Adds a project to the source directory.
    /// </summary>
    /// <param name="project">The project to add.</param>
    public void AddProject(ProjectBase project)
    {
        _projects.Add(project);
    }

    /// <summary>
    /// Adds one or more projects to the source directory.
    /// </summary>
    /// <param name="projects">The projects to add.</param>
    public void AddProjects(params ProjectBase[] projects)
    {
        foreach (var project in projects)
        {
            if (project is null)
            {
                continue;
            }
            _projects.Add(project);
        }
    }

    /// <summary>
    /// Adds a collection of projects to the source directory.
    /// </summary>
    /// <param name="projects">The projects to add.</param>
    public void AddProjects(IEnumerable<ProjectBase> projects)
    {
        foreach (var project in projects)
        {
            _projects.Add(project);
        }
    }
}
