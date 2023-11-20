using System.Text;
using Spectre.Console;

namespace FileExplorer.Main;

public class FileTreeExplorer(string rootDirectory)
{
    private string _previousDirectory = string.Empty;
    private const string _folderIcon = "\uf07b ";
    private const string _prevDirectoryDisplay = _folderIcon + "...";

    public void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        do
        {
            DisplayFileList();
        } while (true);
    }

    private void DisplayFileList()
    {
        var directories = Directory
            .GetFileSystemEntries(rootDirectory)
            .Where(
                entry =>
                    (File.GetAttributes(entry) & FileAttributes.Directory)
                    == FileAttributes.Directory
            )
            .Prepend(_previousDirectory)
            .Select(
                entry =>
                    new
                    {
                        Name = entry,
                        DisplayText = (entry == _previousDirectory)
                            ? _prevDirectoryDisplay // TODO: Currently not used
                            : _folderIcon + entry
                    }
            )
            .ToList();

        if (directories.Skip(1).ToList().Count > 0)
        {
            var tempRootDirectory = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select the directory you'd like to create your[green]project in[/]?")
                    .MoreChoicesText("[grey](Move up and down to reveal more directories)[/]")
                    .AddChoiceGroup("...", directories.First().Name)
                    .AddChoiceGroup("Directories", directories.Skip(1).Select(d => d.DisplayText))
            );
            rootDirectory = tempRootDirectory.Replace(_folderIcon, string.Empty);
            _previousDirectory = Path.GetDirectoryName(rootDirectory)!;
        }
        else
        {
            AnsiConsole.Write("[red]No directories found[/]");
            if (AnsiConsole.Confirm("Return to previous directory"))
            {
                rootDirectory = _previousDirectory!;
                _previousDirectory = Path.GetDirectoryName(rootDirectory)!;
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
