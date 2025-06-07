using SimmerInterviewTask.Features.Meat;
using SimmerInterviewTask.Model;
using Xunit;

namespace Simmer.Tests.Features.Meat;

public class MeatOnlyMenuItemFilterTests
{
    [Fact]
    public void IsAllowed_ShouldThrowArgumentNullException_GivenNullMenuItem()
    {
        // Arrange
        MeatOnlyMenuItemFilter sut = new();
        MenuItem? menuItem = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => sut.IsAllowed(menuItem!));
    }

    [Fact]
    public void IsAllowed_ShouldReturnTrue_GivenAMenuItemWithMeatOnly()
    {
        // Arrange
        MeatOnlyMenuItemFilter sut = new();
        MenuItem menuItem = TestDataHelper.CreateMenuItem(
            isVegan: false,
            isVegetarian: false);

        // Act
        bool result = sut.IsAllowed(menuItem);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public void IsAllowed_ShouldReturnFalse_GivenAMainMenuItemThatIsVeganOrVegtarian(
        bool isVegan,
        bool isVegetarian)
    {
        // Arrange
        MeatOnlyMenuItemFilter sut = new();
        MenuItem menuItem = TestDataHelper.CreateMenuItem(
            isVegan: isVegan,
            isVegetarian: isVegetarian,
            type: MenuItemType.Main
        );

        // Act
        bool result = sut.IsAllowed(menuItem);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(true, true)]
    public void IsAllowed_ShouldReturnTrue_GivenABreakfastMenuItemThatIsVeganOrVegtarian(
        bool isVegan,
        bool isVegetarian)
    {
        // Arrange
        MeatOnlyMenuItemFilter sut = new();
        MenuItem menuItem = TestDataHelper.CreateMenuItem(
            isVegan: isVegan,
            isVegetarian: isVegetarian,
            type: MenuItemType.Breakfast
        );

        // Act
        bool result = sut.IsAllowed(menuItem);

        // Assert
        Assert.True(result);
    }
}
