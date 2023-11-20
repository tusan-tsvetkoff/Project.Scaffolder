using System.Reflection.Metadata;
using FluentAssertions;
using ProjectScaffold.Strategy;
using ProjectScaffold.Utils;

namespace Console.UnitTests.Stylers;

public sealed class DotNetStylerTests
{
    private readonly DotNetStyler _styler = null!;

    public DotNetStylerTests()
    {
        _styler = new DotNetStyler();
    }

    [Fact]
    public void Stylize_Returns_CorrectlyStyledString()
    {
        string directory = @"C:\TestDirectory";
        string fileName = "TestFile.cs";

        string result = _styler.Stylize(directory, fileName);
        string expected = _styler.FullStyle;

        result.Should().Be(expected);
    }

    [Theory]
    [
        InlineData(".cs"),
        InlineData(".csproj"),
        InlineData(".sln"),
        InlineData(".razor"),
        InlineData(".cshtml")
    ]
    public void Icon_ReturnsCorrectIconBasedOnExtension(string extension)
    {
        // Arrange
        string directory = @"C:\TestDirectory";
        string fileName = $"TestFile{extension}";

        // Act
        _styler.Stylize(directory, fileName);
        string result = _styler.Icon;

        // Assert
        result.Should().Be(IconMappingService.Instance.GetIcon<DotNetStyler>(_styler.Extension));
    }

    [Fact]
    public void Stylize_SetsFileNameAndExtension()
    {
        string directory = @"C:\TestDirectory";
        string fileName = "TestFile.cs";

        _styler.Stylize(directory, fileName);

        _styler.FileName.Should().Be(Path.GetRelativePath(directory, fileName));
        _styler.Extension.Should().Be(Path.GetExtension(fileName));
    }

    [Theory]
    [InlineData(".cs", "[blue]")]
    [InlineData(".csproj", "[purple_1]")]
    [InlineData(".sln", "[darkmagenta]")]
    [InlineData(".txt", "[blue]")]
    public void Color_ReturnsCorrectColorBasedOnExtension(string extension, string expectedColor)
    {
        string file = $"TestFile{extension}";
        string directory = @"C:\TestDirectory";

        _styler.Stylize(directory, file);

        _styler.Color.Should().Be(expectedColor);
    }
}
