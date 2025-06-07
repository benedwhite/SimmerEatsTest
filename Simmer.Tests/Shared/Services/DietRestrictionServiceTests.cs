using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Factories;
using SimmerInterviewTask.Shared.Services;
using Xunit;

namespace Simmer.Tests.Shared.Services;

public class DietRestrictionServiceTests
{
    /**
     * Due to time constraints, I have included a simple test case below.
     * However, I have tested the individual filter strategies in their respective test files.
     */

    [Fact]
    public void AllowedByPreferences_()
    {
        // Arrange
        SubscriptionContext subscriptionContext = TestDataHelper.CreateSubscriptionContext();
        IMenuItemFilterFactory menuItemFilterFactory = new MenuItemFilterFactory();
        MenuItem menuItem = TestDataHelper.CreateMenuItem();

        DietRestrictionService sut = new(subscriptionContext, menuItemFilterFactory);

        // Act
        bool result = sut.AllowedByPreferences(menuItem);

        // Assert
        Assert.True(result);
    }
}
