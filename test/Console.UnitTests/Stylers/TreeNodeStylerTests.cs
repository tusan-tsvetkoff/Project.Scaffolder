using System.Reflection;
using FluentAssertions;
using ProjectScaffold.Strategy;

namespace Console.UnitTests.Stylers;

public sealed class TreeNodeStylerTests
{
    private const string _stylerFieldName = "_styler";

    [Theory]
    [InlineData("File.cs", typeof(DotNetStyler))]
    [InlineData("File.txt", typeof(DefaultStyler))]
    [InlineData("File.json", typeof(ConfigStyler))]
    public void SetStyler_InvokesCorrectStyler(string file, Type expectedStylerType)
    {
        // Act
        var styler = new TreeNodeStyler();
        styler.SetStyler(file);

        // Assert
        GetPrivateField(styler, _stylerFieldName).Should().BeOfType(expectedStylerType);
    }

    [Fact]
    public void SetStyler_Should_InvokeDirectoryStyler_When_Directory()
    {
        // Arrange
        var styler = new TreeNodeStyler();
        var directory = Directory.GetDirectories(Directory.GetCurrentDirectory()).First()!;

        // Act
        styler.SetStyler(directory);

        // Assert
        GetPrivateField(styler, _stylerFieldName).Should().BeOfType<DirectoryStyler>();
    }

    private static object GetPrivateField(object obj, string fieldName)
    {
        var field = obj.GetType()
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        return field!.GetValue(obj)!; //! is safe because we know the field exists, and will never be null
    }
}
