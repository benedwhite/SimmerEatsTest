using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Factories;
using SimmerInterviewTask.Shared.Services.Abstractions;
using static SimmerInterviewTask.Model.SubscriptionContext;

namespace SimmerInterviewTask.Shared.Services;

internal sealed class DietRestrictionService(
    DietaryPreferences dietaryPreferences,
    IMenuItemFilterFactory dietMenuItemStrategyFactory,
    IMenuItemBlockedIngredientsChecker menuItemBlockedIngredientsChecker)
    : IDietRestrictionService
{
    private readonly DietaryPreferences _dietaryPreferences = dietaryPreferences
        ?? throw new ArgumentNullException(nameof(dietaryPreferences));

    private readonly IMenuItemFilterFactory _dietMenuItemStrategyFactory = dietMenuItemStrategyFactory
        ?? throw new ArgumentNullException(nameof(dietMenuItemStrategyFactory));

    private readonly IMenuItemBlockedIngredientsChecker _menuItemBlockedIngredientsChecker = menuItemBlockedIngredientsChecker
        ?? throw new ArgumentNullException(nameof(menuItemBlockedIngredientsChecker));

    public bool AllowedByPreferences(MenuItem menuItem)
    {
        ArgumentNullException.ThrowIfNull(menuItem);

        Diet? diet = _dietaryPreferences.Diet;
        IMenuItemFilter menuItemFilter = _dietMenuItemStrategyFactory.CreateFrom(diet);

        bool isAllowedByDiet = menuItemFilter.IsAllowed(menuItem);
        bool itemsContainsBlockedIngredients = _menuItemBlockedIngredientsChecker.ContainsBlockedIngredients(
            _dietaryPreferences.BlockedIngredientIds, 
            menuItem);

        return isAllowedByDiet && !itemsContainsBlockedIngredients;
    }
}
