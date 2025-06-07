using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Factories;
using SimmerInterviewTask.Shared.Services.Abstractions;

namespace SimmerInterviewTask.Shared.Services;

internal sealed class DietRestrictionService(
    ICollection<int> blockedIngredientIds,
    IFactory<IMenuItemFilter> dietMenuItemStrategyFactory,
    IMenuItemBlockedIngredientsChecker menuItemBlockedIngredientsChecker)
    : IDietRestrictionService
{
    private readonly ICollection<int> _blockedIngredientIds = blockedIngredientIds
        ?? throw new ArgumentNullException(nameof(blockedIngredientIds));

    private readonly IFactory<IMenuItemFilter> _dietMenuItemStrategyFactory = dietMenuItemStrategyFactory
        ?? throw new ArgumentNullException(nameof(dietMenuItemStrategyFactory));

    private readonly IMenuItemBlockedIngredientsChecker _menuItemBlockedIngredientsChecker = menuItemBlockedIngredientsChecker
        ?? throw new ArgumentNullException(nameof(menuItemBlockedIngredientsChecker));

    public bool AllowedByPreferences(MenuItem menuItem)
    {
        ArgumentNullException.ThrowIfNull(menuItem);

        IMenuItemFilter menuItemFilter = _dietMenuItemStrategyFactory.Create();

        bool isAllowedByDiet = menuItemFilter.IsAllowed(menuItem);
        bool itemsContainsBlockedIngredients = _menuItemBlockedIngredientsChecker.ContainsBlockedIngredients(
            _blockedIngredientIds,
            menuItem);

        return isAllowedByDiet && !itemsContainsBlockedIngredients;
    }
}
