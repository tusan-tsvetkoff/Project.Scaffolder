using System.Reflection;
using FluentAssertions;
using ProjectScaffold.Strategy;
using ProjectScaffold.Utils;

namespace Console.UnitTests.Stylers;

public sealed class DefaultStylerTests
{
    private readonly DefaultStyler _styler = null!;

    public DefaultStylerTests()
    {
        _styler = new DefaultStyler();
    }

    [Fact]
    public void Stylize_Returns_CorrectlyStyledString()
    {
        string directory = @"C:\TestDirectory";
        string fileName = "TestFile.cs";

        string result = _styler.Stylize(directory, fileName);
        string expected = _styler.FullStyle;

        result.Should().NotBeNullOrWhiteSpace();
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(".toml")]
    [InlineData(".exe")]
    public void Icon_ReturnsCorrectIconBasedOnExtension(string extension)
    {
        // Arrange
        string directory = @"C:\TestDirectory";
        string fileName = $"TestFile{extension}";

        // Act
        _styler.Stylize(directory, fileName);
        string result = _styler.Icon;

        // Assert
        result.Should().NotBeNullOrWhiteSpace();
        result.Should().Be(IconMappingService.Instance.GetIcon<DefaultStyler>(extension));
    }

    [Fact]
    public void Stylize_SetsFileName()
    {
        string directory = @"C:\TestDirectory";
        string fileName = "TestFile.toml"; // <- don't have .toml configured yet, so using it to test

        _styler.Stylize(directory, fileName);

        _styler.FileName.Should().NotBeNullOrWhiteSpace();
        _styler.FileName.Should().Be(Path.GetRelativePath(directory, fileName));
    }

    [Fact]
    public void Stylize_SetsCorrectFullStyle()
    {
        string directory = @"C:\TestDirectory";
        string fileName = $"TestFile.toml";

        _styler.Stylize(directory, fileName);

        _styler.FullStyle.Should().NotBeNullOrWhiteSpace();
        _styler.FullStyle
            .Should()
            .Be($"[silver]{_styler.Icon} {Path.GetRelativePath(directory, fileName)}[/]");
    }
}
