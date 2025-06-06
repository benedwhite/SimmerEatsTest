using SimmerInterviewTask.Model;
using SimmerInterviewTask.Service;
using Xunit;

namespace Simmer.Tests
{
    public sealed class ExampleChoiceSuggestionServiceTests
    {
        private readonly ExampleChoiceSuggestionService _sut = new();

        [Theory]
        [InlineData(true, true)]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [InlineData(false, false)]
        public void SuggestChoicesFor_IncludesVeganVegetarianAndMeatBreakfasts_WhenDietIsMeatOnly(
            bool isVegan, 
            bool isVegetarian)
        {
            // Arrange
            MenuItem menuItem = new()
            {
                Id = 1,
                IsVegan = isVegan,
                IsVegetarian = isVegetarian,
                Type = MenuItemType.Breakfast,
                IngredientIds = [1, 2, 3]
            };

            SubscriptionContext.DietaryPreferences dietaryPreferences = new()
            {
                Diet = Diet.MeatOnly,
                BlockedIngredientIds = []
            };

            MenuContext menuContext = new()
            {
                MenuItems = [menuItem],
                RecommendedMenuItemIds = [menuItem.Id]
            };

            SubscriptionContext subscriptionContext = new()
            {
                LatestReviews = [],
                DietPreferences = dietaryPreferences,
                PreviouslyOrderedMenuItems = []
            };

            ChoiceAllocation choiceAllocation = new()
            {
                MainsPermitted = 1,
                BreakfastsPermitted = 1,
                MainPortionSize = MainPortionSize.Standard
            };

            // Act
            ICollection<EntryChoice> result = _sut.SuggestChoicesFor(
                subscriptionContext,
                menuContext,
                choiceAllocation
            );

            // Assert
            Assert.Contains(result, choice => choice.MenuItemId == menuItem.Id);
        }
    }
}
