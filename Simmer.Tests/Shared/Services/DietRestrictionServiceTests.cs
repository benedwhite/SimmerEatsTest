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
        DietaryPreferences dietaryPreferences = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new DietRestrictionService(
                dietaryPreferences,
                CreateMenuItemFilterFactory(),
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
                TestDataHelper.CreateDietaryPreferences(),
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
                TestDataHelper.CreateDietaryPreferences(),
                CreateMenuItemFilterFactory(),
                menuItemBlockedIngredientsChecker));
    }

    [Fact]
    public void AllowedByPreferences_ShouldThrowArgumentNullException_GivenNullMenuItem()
    {
        // Arrange
        MenuItem menuItem = null!;
        DietRestrictionService sut = new(
            TestDataHelper.CreateDietaryPreferences(),
            CreateMenuItemFilterFactory(),
            CreateMenuItemBlockedIngredientsChecker());

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => sut.AllowedByPreferences(menuItem));
    }

    [Fact]
    public void AllowedByPreferences_ReturnsTrue_WhenDietIsEverythingAndNoBlockedIngredients()
    {
        // Arrange
        DietaryPreferences dietaryPreferences = TestDataHelper.CreateDietaryPreferences(
            Diet.Everything,
            blockedIngredientIds: []);
        MenuItem menuItem = TestDataHelper.CreateMenuItem();

        DietRestrictionService sut = new(
            dietaryPreferences,
            CreateMenuItemFilterFactory(),
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
        DietaryPreferences dietaryPreferences = TestDataHelper.CreateDietaryPreferences(
            Diet.MeatOnly,
            blockedIngredientIds: []);
        MenuItem menuItem = TestDataHelper.CreateMenuItem(isVegan: true);

        DietRestrictionService sut = new(
            dietaryPreferences,
            CreateMenuItemFilterFactory(),
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
        DietaryPreferences dietaryPreferences = TestDataHelper.CreateDietaryPreferences(
            Diet.Everything,
            blockedIngredientIds: [1]);
        MenuItem menuItem = TestDataHelper.CreateMenuItem(ingredientIds: [1]);

        DietRestrictionService sut = new(
            dietaryPreferences,
            CreateMenuItemFilterFactory(),
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
        DietaryPreferences dietaryPreferences = TestDataHelper.CreateDietaryPreferences(
            Diet.Everything,
            blockedIngredientIds: [1]);
        MenuItem menuItem = TestDataHelper.CreateMenuItem(ingredientIds: [2]);

        DietRestrictionService sut = new(
            dietaryPreferences,
            CreateMenuItemFilterFactory(),
            CreateMenuItemBlockedIngredientsChecker());

        // Act
        bool result = sut.AllowedByPreferences(menuItem);

        // Assert
        Assert.True(result);
    }

    private static MenuItemFilterFactory CreateMenuItemFilterFactory() => new();

    private static MenuItemBlockedIngredientsChecker CreateMenuItemBlockedIngredientsChecker() => new();
}
