using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Factories;
using SimmerInterviewTask.Shared.Services;
using Xunit;

namespace Simmer.Tests.Shared.Services;

public class DietRestrictionServiceTests
{
    [Fact]
    public void AllowedByPreferences_ReturnsTrue_WhenDietIsEverythingAndNoBlockedIngredients()
    {
        // Arrange
        SubscriptionContext.DietaryPreferences dietaryPreferences = TestDataHelper.CreateDietaryPreferences(
            Diet.Everything,
            blockedIngredientIds: []);
        SubscriptionContext subscriptionContext = TestDataHelper.CreateSubscriptionContext(dietaryPreferences);
        MenuItem menuItem = TestDataHelper.CreateMenuItem();

        DietRestrictionService sut = new(
            subscriptionContext.DietPreferences,
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
        SubscriptionContext.DietaryPreferences dietaryPreferences = TestDataHelper.CreateDietaryPreferences(
            Diet.MeatOnly,
            blockedIngredientIds: []);
        SubscriptionContext subscriptionContext = TestDataHelper.CreateSubscriptionContext(dietaryPreferences);
        MenuItem menuItem = TestDataHelper.CreateMenuItem(isVegan: true);

        DietRestrictionService sut = new(
            subscriptionContext.DietPreferences,
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
        SubscriptionContext.DietaryPreferences dietaryPreferences = TestDataHelper.CreateDietaryPreferences(
            Diet.Everything,
            blockedIngredientIds: [1]);
        SubscriptionContext subscriptionContext = TestDataHelper.CreateSubscriptionContext(dietaryPreferences);
        MenuItem menuItem = TestDataHelper.CreateMenuItem(ingredientIds: [1]);

        DietRestrictionService sut = new(
            subscriptionContext.DietPreferences,
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
        SubscriptionContext.DietaryPreferences dietaryPreferences = TestDataHelper.CreateDietaryPreferences(
            Diet.Everything,
            blockedIngredientIds: [1]);
        SubscriptionContext subscriptionContext = TestDataHelper.CreateSubscriptionContext(dietaryPreferences);
        MenuItem menuItem = TestDataHelper.CreateMenuItem(ingredientIds: [2]);

        DietRestrictionService sut = new(
            subscriptionContext.DietPreferences,
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
