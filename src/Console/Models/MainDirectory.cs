namespace ProjectScaffold.Models;

internal sealed class MainDirectory
{
    private static readonly Lazy<MainDirectory> lazy = new(() => new MainDirectory());

    public static MainDirectory Instance
    {
        get { return lazy.Value; }
    }
    private readonly List<ProjectBase> _projects = [];
    public List<ProjectBase> Projects => _projects;

    private MainDirectory() { }

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
