using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Factories;
using SimmerInterviewTask.Shared.Services;
using Xunit;
using static SimmerInterviewTask.Model.SubscriptionContext;

namespace Simmer.Tests.Shared.Services;

public class DietRestrictionServiceTests
{
    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_GivenNullMenuItemFilterFactory()
    {
        // Arrange
        MenuItemFilterFactory menuItemFilterFactory = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new DietRestrictionService(
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
                CreateMenuItemFilterFactory(),
                menuItemBlockedIngredientsChecker));
    }

    [Fact]
    public void AllowedByPreferences_ShouldThrowArgumentNullException_GivenNullMenuItem()
    {
        // Arrange
        MenuItem menuItem = null!;
        DietRestrictionService sut = CreateDietRestrictionService();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => sut.AllowedByPreferences(
            menuItem,
            TestDataHelper.CreateDietaryPreferences()));
    }

    [Fact]
    public void AllowedByPreferences_ShouldThrowArgumentNullException_GivenNullDietaryPreferences()
    {
        // Arrange
        DietaryPreferences dietaryPreferences = null!;
        DietRestrictionService sut = CreateDietRestrictionService();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => sut.AllowedByPreferences(
            TestDataHelper.CreateMenuItem(),
            dietaryPreferences));
    }

    [Fact]
    public void AllowedByPreferences_ReturnsTrue_WhenDietIsEverythingAndNoBlockedIngredients()
    {
        // Arrange
        DietaryPreferences dietaryPreferences = TestDataHelper.CreateDietaryPreferences(
            Diet.Everything,
            blockedIngredientIds: []);
        MenuItem menuItem = TestDataHelper.CreateMenuItem();
        DietRestrictionService sut = CreateDietRestrictionService();

        // Act
        bool result = sut.AllowedByPreferences(menuItem, dietaryPreferences);

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
        DietRestrictionService sut = CreateDietRestrictionService();

        // Act
        bool result = sut.AllowedByPreferences(menuItem, dietaryPreferences);

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
        DietRestrictionService sut = CreateDietRestrictionService();

        // Act
        bool result = sut.AllowedByPreferences(menuItem, dietaryPreferences);

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
        DietRestrictionService sut = CreateDietRestrictionService();

        // Act
        bool result = sut.AllowedByPreferences(menuItem, dietaryPreferences);

        // Assert
        Assert.True(result);
    }

    private static DietRestrictionService CreateDietRestrictionService() => new(
        CreateMenuItemFilterFactory(),
        CreateMenuItemBlockedIngredientsChecker());

    private static MenuItemFilterFactory CreateMenuItemFilterFactory() => new();

    private static MenuItemBlockedIngredientsChecker CreateMenuItemBlockedIngredientsChecker() => new();
}
