using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Factories;
using Xunit;

namespace Simmer.Tests.Shared.Factories;

public class RecipeTypeFactoryTests
{
    [Fact]
    public void Create_WithLargePortionSize_ReturnsLargeRecipeType()
    {
        // Arrange
        MainPortionSize mainPortionSize = MainPortionSize.Large;
        RecipeTypeFactory sut = CreateRecipeTypeFactory();

        // Act
        RecipeType result = sut.CreateFrom(mainPortionSize);

        // Assert
        Assert.Equal(RecipeType.Large, result);
    }

    [Theory]
    [InlineData(MainPortionSize.Standard)]
    [InlineData(MainPortionSize.Lean)]
    public void Create_WithOtherPortionSizes_ReturnsStandardRecipeType(MainPortionSize mainPortionSize)
    {
        // Arrange
        RecipeTypeFactory sut = CreateRecipeTypeFactory();

        // Act
        RecipeType result = sut.CreateFrom(mainPortionSize);

        // Assert
        Assert.Equal(RecipeType.Standard, result);
    }

    private static RecipeTypeFactory CreateRecipeTypeFactory() => new();
}
