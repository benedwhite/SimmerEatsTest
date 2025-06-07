using SimmerInterviewTask.Core;
using SimmerInterviewTask.Model;
using Xunit;

namespace Simmer.Tests.Shared;

public class DefaultMenuItemFilterTests
{
    [Fact]
    public void IsAllowed_ShouldReturnTrue_GivenAMenuItem()
    {
        // Arrange
        DefaultMenuItemFilter sut = new();
        MenuItem menuItem = new()
        {
            ExcludeFromSuggestions = false,
            Id = 1,
            IngredientIds = [1, 2, 3],
            IsVegan = false,
            IsVegetarian = false,
            Type = MenuItemType.Main
        };

        // Act
        bool result = sut.IsAllowed(menuItem);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsAllowed_ShouldReturnTrue_GivenNull()
    {
        // Arrange
        DefaultMenuItemFilter sut = new();
        MenuItem menuItem = null!;

        // Act
        bool result = sut.IsAllowed(menuItem);

        // Assert
        Assert.True(result);
    }
}
