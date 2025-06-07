using SimmerInterviewTask.Features.Vegan;
using SimmerInterviewTask.Model;
using Xunit;

namespace Simmer.Tests.Features.Vegan;

public class VeganMenuItemFilterTests
{
    [Fact]
    public void IsAllowed_ShouldThrowArgumentNullException_GivenNullMenuItem()
    {
        // Arrange
        VeganMenuItemFilter sut = new();
        MenuItem? menuItem = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => sut.IsAllowed(menuItem!));
    }

    [Fact]
    public void IsAllowed_ShouldReturnTrue_GivenAVeganMenuItem()
    {
        // Arrange
        VeganMenuItemFilter sut = new();
        MenuItem menuItem = TestDataHelper.CreateMenuItem(
            isVegan: true,
            isVegetarian: true);

        // Act
        bool result = sut.IsAllowed(menuItem);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsAllowed_ShouldReturnFalse_GivenAMenuItemThatIsNotVegan()
    {
        // Arrange
        VeganMenuItemFilter sut = new();
        MenuItem menuItem = TestDataHelper.CreateMenuItem(
            isVegan: false,
            isVegetarian: true);

        // Act
        bool result = sut.IsAllowed(menuItem);

        // Assert
        Assert.False(result);
    }
}
