using SimmerInterviewTask.Model;
using SimmerInterviewTask.Services;
using Xunit;

namespace Simmer.Tests.Services
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
            SubscriptionContext.DietaryPreferences dietaryPreferences = TestDataHelper.CreateDietaryPreferences(
                diet: Diet.Everything);
            SubscriptionContext subscriptionContext = TestDataHelper.CreateSubscriptionContext(dietaryPreferences);
            MenuItem menuItem = TestDataHelper.CreateMenuItem(
                isVegan: isVegan,
                isVegetarian: isVegetarian);
            MenuContext menuContext = TestDataHelper.CreateMenuContext(
                [menuItem], 
                []);
            ChoiceAllocation choiceAllocation = TestDataHelper.CreateChoiceAllocation();

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
