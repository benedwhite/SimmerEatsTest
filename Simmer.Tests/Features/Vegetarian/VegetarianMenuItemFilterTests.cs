using SimmerInterviewTask.Features.Vegetarian;
using SimmerInterviewTask.Model;
using Xunit;

namespace Simmer.Tests.Features.Vegan;

public class VegetarianMenuItemFilterTests
{
    [Fact]
    public void IsAllowed_ShouldThrowArgumentNullException_GivenNullMenuItem()
    {
        // Arrange
        VegetarianMenuItemFilter sut = new();
        MenuItem? menuItem = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => sut.IsAllowed(menuItem!));
    }

    [Fact]
    public void IsAllowed_ShouldReturnTrue_GivenAVegetarianMenuItem()
    {
        // Arrange
        VegetarianMenuItemFilter sut = new();
        MenuItem menuItem = TestDataHelper.CreateMenuItem(
            isVegan: false,
            isVegetarian: true);

        // Act
        bool result = sut.IsAllowed(menuItem);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsAllowed_ShouldReturnFalse_GivenAMenuItemThatIsNotVegetarian()
    {
        // Arrange
        VegetarianMenuItemFilter sut = new();
        MenuItem menuItem = TestDataHelper.CreateMenuItem(
            isVegan: false,
            isVegetarian: false);

        // Act
        bool result = sut.IsAllowed(menuItem);

        // Assert
        Assert.False(result);
    }
}
