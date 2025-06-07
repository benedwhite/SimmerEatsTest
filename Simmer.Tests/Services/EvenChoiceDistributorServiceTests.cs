using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Factories;
using SimmerInterviewTask.Shared.Services;
using Xunit;

namespace Simmer.Tests.Services;

public class EvenChoiceDistributorServiceTests
{
    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_GivenNullRecipeTypeFactory()
    {
        // Arrange
        RecipeTypeFactory recipeTypeFactory = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new EvenChoiceDistributorService(
                recipeTypeFactory,
                CreateEvenDistributorCalculator()));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_GivenNullEvenDistributorCalculator()
    {
        // Arrange
        EvenDistributorCalculator evenDistributorCalculator = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new EvenChoiceDistributorService(
                CreateRecipeTypeFactory(),
                evenDistributorCalculator));
    }

    [Fact]
    public void DistributeChoicesEvenly_ShouldThrowArgumentNullException_GivenNullMenuItems()
    {
        // Arrange
        EvenChoiceDistributorService sut = CreateEvenChoiceDistributorService();
        ICollection<MenuItem> menuItems = null!;
        int count = 2;
        MainPortionSize portionSize = MainPortionSize.Standard;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => sut.DistributeChoicesEvenly(
            menuItems,
            count,
            portionSize));
    }

    [Fact]
    public void DistributeChoicesEvenly_ShouldReturnEmptyCollection_GivenEmptyMenuItems()
    {
        // Arrange
        EvenChoiceDistributorService sut = CreateEvenChoiceDistributorService();
        ICollection<MenuItem> menuItems = [];
        int count = 2;
        MainPortionSize portionSize = MainPortionSize.Standard;

        // Act
        IEnumerable<EntryChoice> choices = sut.DistributeChoicesEvenly(
            menuItems,
            count,
            portionSize);

        // Assert
        Assert.Empty(choices);
    }

    [Fact]
    public void DistributeChoicesEvenly_ShouldDistributeTotalQuantityAcrossAllMenuItems_GivenMenuItemsACountAndPortionSize()
    {
        // Arrange
        EvenChoiceDistributorService sut = CreateEvenChoiceDistributorService();
        ICollection<MenuItem> menuItems = [
            TestDataHelper.CreateMenuItem(id: 1),
            TestDataHelper.CreateMenuItem(id: 2, menuItemType: MenuItemType.Breakfast),
            TestDataHelper.CreateMenuItem(id: 3, isVegetarian: true)];
        int count = 10;
        MainPortionSize portionSize = MainPortionSize.Standard;

        // Act
        IEnumerable<EntryChoice> result = sut.DistributeChoicesEvenly(
            menuItems,
            count,
            portionSize);

        // Assert
        Assert.Equal(count, result.Sum(entryChoice => entryChoice.Quantity));
    }

    private static EvenChoiceDistributorService CreateEvenChoiceDistributorService()
        => new(CreateRecipeTypeFactory(), CreateEvenDistributorCalculator());

    private static RecipeTypeFactory CreateRecipeTypeFactory() => new();

    private static EvenDistributorCalculator CreateEvenDistributorCalculator() => new();
}
