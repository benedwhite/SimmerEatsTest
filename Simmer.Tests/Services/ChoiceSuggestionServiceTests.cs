using SimmerInterviewTask.Model;
using SimmerInterviewTask.Services;
using SimmerInterviewTask.Shared.Factories;
using SimmerInterviewTask.Shared.Services;
using Xunit;

namespace Simmer.Tests.Services
{
    public sealed class ChoiceSuggestionServiceTests
    {
        [Fact]
        public void SuggestChoicesFor_IncludesVeganVegetarianAndMeatBreakfasts_WhenDietIsMeatOnly()
        {
            // Arrange
            SubscriptionContext.DietaryPreferences dietaryPreferences = TestDataHelper.CreateDietaryPreferences(
                diet: Diet.Vegan,
                blockedIngredientIds: [4]);

            SubscriptionContext subscriptionContext = TestDataHelper.CreateSubscriptionContext(
                dietaryPreferences,
                veganRatio: 0.5m);

            MenuContext menuContext = TestDataHelper.CreateMenuContext(
                [
                    TestDataHelper.CreateMenuItem(
                        id: 1,
                        isVegan: false,
                        isVegetarian: false,
                        menuItemType: MenuItemType.Breakfast),
                    TestDataHelper.CreateMenuItem(
                        id: 2,
                        isVegan: false,
                        isVegetarian: false,
                        menuItemType: MenuItemType.Main),
                    TestDataHelper.CreateMenuItem(
                        id: 3,
                        isVegan: true,
                        isVegetarian: false,
                        menuItemType: MenuItemType.Breakfast,
                        ingredientIds: [4]),
                    TestDataHelper.CreateMenuItem(
                        id: 4,
                        isVegan: true,
                        isVegetarian: false,
                        menuItemType: MenuItemType.Main)
                ], 
                []);

            ChoiceAllocation choiceAllocation = TestDataHelper.CreateChoiceAllocation(
                breakfastsPermitted: 8,
                mainsPermitted: 6);

            ChoiceSuggestionService sut = new(
                new DietRestrictionService(
                    new MenuItemFilterFactory(),
                    new MenuItemBlockedIngredientsChecker()),
                new EntryChoicePickerFactory(
                    new EvenChoiceDistributorService(
                        new RecipeTypeFactory(),
                        new EvenDistributorCalculator()),
                    new RatioSplitter()),
                new DictionaryMenuItemTypeCountService(),
                [MenuItemType.Main, MenuItemType.Breakfast]);

            // Act
            ICollection<EntryChoice> result = sut.SuggestChoicesFor(
                subscriptionContext,
                menuContext,
                choiceAllocation
            );

            // Assert
        }

        [Fact]
        public void SuggestionsAreReturned_AsExpected()
        {
            // Add your test code here.
            // These are just placeholders, feel free to tweak as you like
        }

        [Fact]
        public void BlockedIngredientMeal_IsNotReturned()
        {
            // Add your test code here.
            // These are just placeholders, feel free to tweak as you like
        }

        [Fact]
        public void OnlyMatchingDietMeals_AreReturned()
        {
            // Add your test code here.
            // These are just placeholders, feel free to tweak as you like
        }

        [Fact]
        public void WeeklyRecommendedMeals_AreIncluded()
        {
            // Add your test code here.
            // These are just placeholders, feel free to tweak as you like
        }

        [Fact]
        public void MoreThanOnePreviouslyOrderedMeal_AreIncluded()
        {
            // Add your test code here.
            // These are just placeholders, feel free to tweak as you like
        }

        [Fact]
        public void HighRatedMeals_AreIncluded()
        {
            // Add your test code here.
            // These are just placeholders, feel free to tweak as you like
        }
    }
}
