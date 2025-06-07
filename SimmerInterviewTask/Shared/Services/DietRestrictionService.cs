using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Factories;
using SimmerInterviewTask.Shared.Services.Abstractions;

namespace SimmerInterviewTask.Shared.Services;

internal sealed class DietRestrictionService(
    SubscriptionContext subscriptionContext,
    IMenuItemFilterFactory dietMenuItemStrategyFactory)
    : IDietRestrictionService
{
    private readonly SubscriptionContext _subscriptionContext = subscriptionContext
        ?? throw new ArgumentNullException(nameof(subscriptionContext));

    private readonly IMenuItemFilterFactory _dietMenuItemStrategyFactory = dietMenuItemStrategyFactory
        ?? throw new ArgumentNullException(nameof(dietMenuItemStrategyFactory));

    public bool AllowedByPreferences(MenuItem menuItem)
    {
        ArgumentNullException.ThrowIfNull(menuItem);
        ArgumentNullException.ThrowIfNull(_subscriptionContext.DietPreferences);

        Diet? diet = _subscriptionContext.DietPreferences.Diet;
        IMenuItemFilter menuItemFilter = _dietMenuItemStrategyFactory.CreateFor(diet);

        return menuItemFilter.IsAllowed(menuItem);
    }
}
