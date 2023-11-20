namespace ProjectScaffold.Common.Abstractions;
internal interface ITreeNodeStyler
{
    string Icon { get; }
    string FileName { get; }
    string Color {get;}
    string FullStyle {get;}

   string Stylize(string directory, string file);
}