using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Factories.Abstractions;
using SimmerInterviewTask.Shared.Services.Abstractions;
using static SimmerInterviewTask.Model.SubscriptionContext;

namespace SimmerInterviewTask.Shared.Services;

internal sealed class DietRestrictionService(
    IMenuItemFilterFactory menuItemFilterFactory,
    IMenuItemBlockedIngredientsChecker menuItemBlockedIngredientsChecker)
    : IDietRestrictionService
{
    private readonly IMenuItemFilterFactory _menuItemFilterFactory = menuItemFilterFactory
        ?? throw new ArgumentNullException(nameof(menuItemFilterFactory));

    private readonly IMenuItemBlockedIngredientsChecker _menuItemBlockedIngredientsChecker = menuItemBlockedIngredientsChecker
        ?? throw new ArgumentNullException(nameof(menuItemBlockedIngredientsChecker));

    public bool AllowedByPreferences(
        MenuItem menuItem, 
        DietaryPreferences dietaryPreferences)
    {
        ArgumentNullException.ThrowIfNull(menuItem);
        ArgumentNullException.ThrowIfNull(dietaryPreferences);

        Diet? diet = dietaryPreferences.Diet;
        IMenuItemFilter menuItemFilter = _menuItemFilterFactory.CreateFrom(diet);

        bool isAllowedByDiet = menuItemFilter.IsAllowed(menuItem);
        bool itemsContainsBlockedIngredients = _menuItemBlockedIngredientsChecker.ContainsBlockedIngredients(
            dietaryPreferences.BlockedIngredientIds, 
            menuItem);

        return isAllowedByDiet && !itemsContainsBlockedIngredients;
    }
}
