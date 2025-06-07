using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Factories;
using SimmerInterviewTask.Shared.Services;
using Xunit;
using static SimmerInterviewTask.Model.SubscriptionContext;

namespace Simmer.Tests.Shared.Services;

public class DietRestrictionServiceTests
{
    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_GivenNullDietPreferences()
    {
        // Arrange
        ICollection<int> blockedIngredientIds = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new DietRestrictionService(
                blockedIngredientIds,
                CreateMenuItemFilterFactory(Diet.Everything),
                CreateMenuItemBlockedIngredientsChecker()));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_GivenNullMenuItemFilterFactory()
    {
        // Arrange
        MenuItemFilterFactory menuItemFilterFactory = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new DietRestrictionService(
                [],
                menuItemFilterFactory,
                CreateMenuItemBlockedIngredientsChecker()));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_GivenNullMenuItemBlockedIngredientsChecker()
    {
        // Arrange
        MenuItemBlockedIngredientsChecker menuItemBlockedIngredientsChecker = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new DietRestrictionService(
                [],
                CreateMenuItemFilterFactory(Diet.Everything),
                menuItemBlockedIngredientsChecker));
    }

    [Fact]
    public void AllowedByPreferences_ShouldThrowArgumentNullException_GivenNullMenuItem()
    {
        // Arrange
        MenuItem menuItem = null!;
        DietRestrictionService sut = new(
            [],
            CreateMenuItemFilterFactory(Diet.Everything),
            CreateMenuItemBlockedIngredientsChecker());

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => sut.AllowedByPreferences(menuItem));
    }

    [Fact]
    public void AllowedByPreferences_ReturnsTrue_WhenDietIsEverythingAndNoBlockedIngredients()
    {
        // Arrange
        Diet diet = Diet.Everything;
        ICollection<int> blockedIngredientIds = [];
        MenuItem menuItem = TestDataHelper.CreateMenuItem();

        DietRestrictionService sut = new(
            blockedIngredientIds,
            CreateMenuItemFilterFactory(diet),
            CreateMenuItemBlockedIngredientsChecker());

        // Act
        bool result = sut.AllowedByPreferences(menuItem);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void AllowedByPreferences_ReturnsFalse_WhenDietIsMeatOnlyAndMenuItemIsVegan()
    {
        // Arrange
        Diet diet = Diet.MeatOnly;
        ICollection<int> blockedIngredientIds = [];
        MenuItem menuItem = TestDataHelper.CreateMenuItem(isVegan: true);

        DietRestrictionService sut = new(
            blockedIngredientIds,
            CreateMenuItemFilterFactory(diet),
            CreateMenuItemBlockedIngredientsChecker());

        // Act
        bool result = sut.AllowedByPreferences(menuItem);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AllowedByPreferences_ReturnsFalse_WhenDietIsEverythingAndContainsBlockedIngredients()
    {
        // Arrange
        Diet diet = Diet.Everything;
        ICollection<int> blockedIngredientIds = [1];
        MenuItem menuItem = TestDataHelper.CreateMenuItem(ingredientIds: [1]);

        DietRestrictionService sut = new(
            blockedIngredientIds,
            CreateMenuItemFilterFactory(diet),
            CreateMenuItemBlockedIngredientsChecker());

        // Act
        bool result = sut.AllowedByPreferences(menuItem);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AllowedByPreferences_ReturnsTrue_WhenDietIsEverythingAndDoesNotContainBlockedIngredients()
    {
        // Arrange
        Diet diet = Diet.Everything;
        ICollection<int> blockedIngredientIds = [1];
        MenuItem menuItem = TestDataHelper.CreateMenuItem(ingredientIds: [2]);

        DietRestrictionService sut = new(
            blockedIngredientIds,
            CreateMenuItemFilterFactory(diet),
            CreateMenuItemBlockedIngredientsChecker());

        // Act
        bool result = sut.AllowedByPreferences(menuItem);

        // Assert
        Assert.True(result);
    }

    private static MenuItemFilterFactory CreateMenuItemFilterFactory(Diet? diet) => new(diet);

    private static MenuItemBlockedIngredientsChecker CreateMenuItemBlockedIngredientsChecker() => new();
}
