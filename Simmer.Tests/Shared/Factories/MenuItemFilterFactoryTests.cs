using SimmerInterviewTask.Features.Meat;
using SimmerInterviewTask.Features.Vegan;
using SimmerInterviewTask.Features.Vegetarian;
using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared;
using SimmerInterviewTask.Shared.Factories;
using Xunit;

namespace Simmer.Tests.Shared.Factories;
public class MenuItemFilterFactoryTests
{
    private readonly MenuItemFilterFactory _sut = new();

    [Fact]
    public void CreateFor_WithVeganDiet_ReturnsVeganMenuItemFilter()
    {
        // Arrange
        Diet diet = Diet.Vegan;

        // Act
        IMenuItemFilter result = _sut.CreateFor(diet);

        // Assert
        Assert.IsType<VeganMenuItemFilter>(result);
    }

    [Fact]
    public void CreateFor_WithVegetarianDiet_ReturnsVegetarianMenuItemFilter()
    {
        // Arrange
        Diet diet = Diet.Vegetarian;

        // Act
        IMenuItemFilter result = _sut.CreateFor(diet);

        // Assert
        Assert.IsType<VegetarianMenuItemFilter>(result);
    }

    [Fact]
    public void CreateFor_WithMeatOnlyDiet_ReturnsMeatOnlyMenuItemFilter()
    {
        // Arrange
        Diet diet = Diet.MeatOnly;

        // Act
        IMenuItemFilter result = _sut.CreateFor(diet);

        // Assert
        Assert.IsType<MeatOnlyMenuItemFilter>(result);
    }

    [Theory]
    [InlineData(Diet.Everything)]
    [InlineData(null)]
    public void CreateFor_EverythingOrNullDiet_ReturnsDefaultMenuItemFilter(Diet? diet)
    {
        // Act
        IMenuItemFilter result = _sut.CreateFor(diet);

        // Assert
        Assert.IsType<DefaultMenuItemFilter>(result);
    }
}
