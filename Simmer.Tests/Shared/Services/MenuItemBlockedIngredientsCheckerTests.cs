using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Services;
using Xunit;

namespace Simmer.Tests.Shared.Services
{
    public class MenuItemBlockedIngredientsCheckerTests
    {
        private readonly MenuItemBlockedIngredientsChecker _sut = new();

        [Fact]
        public void ContainsBlockedIngredients_ShouldThrowArgumentNullException_GivenNullMenuItem()
        {
            // Arrange
            MenuItem? menuItem = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _sut.ContainsBlockedIngredients(menuItem!, []));
        }

        [Theory]
        [InlineData(null)]
        [InlineData(new int[] { })]
        public void ContainsBlockedIngredients_ReturnsFalse_WhenBlockedIngredientIdsIsNull(
            ICollection<int>? blockedIngredientIds)
        {
            // Arrange
            MenuItem menuItem = TestDataHelper.CreateMenuItem();

            // Act
            var result = _sut.ContainsBlockedIngredients(menuItem, blockedIngredientIds!);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ContainsBlockedIngredients_ReturnsTrue_WhenBlockIngredientsIdsMatchAnyMenuItemIngredientIds()
        {
            // Arrange
            ICollection<int> blockedIngredientIds = [1, 2, 3];
            MenuItem menuItem = TestDataHelper.CreateMenuItem(ingredientIds: [1]);

            // Act
            var result = _sut.ContainsBlockedIngredients(menuItem, blockedIngredientIds);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ContainsBlockedIngredients_ReturnsFalse_WhenBlockIngredientsIdsDoNotMatchAnyMenuItemIngredientIds()
        {
            // Arrange
            ICollection<int> blockedIngredientIds = [1, 2, 3];
            MenuItem menuItem = TestDataHelper.CreateMenuItem(ingredientIds: [4, 5, 6]);

            // Act
            var result = _sut.ContainsBlockedIngredients(menuItem, blockedIngredientIds);

            // Assert
            Assert.False(result);
        }
    }
}
