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
    [Fact]
    public void Create_WithVeganDiet_ReturnsVeganMenuItemFilter()
    {
        // Arrange
        Diet diet = Diet.Vegan;
        MenuItemFilterFactory sut = CreateMenuItemFilterFactory();

        // Act
        IMenuItemFilter result = sut.CreateFrom(diet);

        // Assert
        Assert.IsType<VeganMenuItemFilter>(result);
    }

    [Fact]
    public void Create_WithVegetarianDiet_ReturnsVegetarianMenuItemFilter()
    {
        // Arrange
        Diet diet = Diet.Vegetarian;
        MenuItemFilterFactory sut = CreateMenuItemFilterFactory();

        // Act
        IMenuItemFilter result = sut.CreateFrom(diet);

        // Assert
        Assert.IsType<VegetarianMenuItemFilter>(result);
    }

    [Fact]
    public void Create_WithMeatOnlyDiet_ReturnsMeatOnlyMenuItemFilter()
    {
        // Arrange
        Diet diet = Diet.MeatOnly;
        MenuItemFilterFactory sut = CreateMenuItemFilterFactory();

        // Act
        IMenuItemFilter result = sut.CreateFrom(diet);

        // Assert
        Assert.IsType<MeatOnlyMenuItemFilter>(result);
    }

    [Theory]
    [InlineData(Diet.Everything)]
    [InlineData(null)]
    public void Create_EverythingOrNullDiet_ReturnsDefaultMenuItemFilter(Diet? diet)
    {
        // Arrange
        MenuItemFilterFactory sut = CreateMenuItemFilterFactory();

        // Act
        IMenuItemFilter result = sut.CreateFrom(diet);

        // Assert
        Assert.IsType<DefaultMenuItemFilter>(result);
    }

    private static MenuItemFilterFactory CreateMenuItemFilterFactory() => new();
}
