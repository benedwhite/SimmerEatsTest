using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Factories;
using SimmerInterviewTask.Shared.Services.Abstractions;
using static SimmerInterviewTask.Model.SubscriptionContext;

namespace SimmerInterviewTask.Shared.Services;

internal sealed class DietRestrictionService(
    IMenuItemFilterFactory dietMenuItemStrategyFactory,
    IMenuItemBlockedIngredientsChecker menuItemBlockedIngredientsChecker)
    : IDietRestrictionService
{
    private readonly IMenuItemFilterFactory _dietMenuItemStrategyFactory = dietMenuItemStrategyFactory
        ?? throw new ArgumentNullException(nameof(dietMenuItemStrategyFactory));

    private readonly IMenuItemBlockedIngredientsChecker _menuItemBlockedIngredientsChecker = menuItemBlockedIngredientsChecker
        ?? throw new ArgumentNullException(nameof(menuItemBlockedIngredientsChecker));

    public bool AllowedByPreferences(MenuItem menuItem, DietaryPreferences dietaryPreferences)
    {
        ArgumentNullException.ThrowIfNull(menuItem);
        ArgumentNullException.ThrowIfNull(dietaryPreferences);

        Diet? diet = dietaryPreferences.Diet;
        IMenuItemFilter menuItemFilter = _dietMenuItemStrategyFactory.CreateFrom(diet);

        bool isAllowedByDiet = menuItemFilter.IsAllowed(menuItem);
        bool itemsContainsBlockedIngredients = _menuItemBlockedIngredientsChecker.ContainsBlockedIngredients(
            dietaryPreferences.BlockedIngredientIds, 
            menuItem);

        return isAllowedByDiet && !itemsContainsBlockedIngredients;
    }
}
