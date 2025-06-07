using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Services.Abstractions;

namespace SimmerInterviewTask.Shared.Services;

internal sealed class MenuItemBlockedIngredientsChecker : IMenuItemBlockedIngredientsChecker
{
    public bool ContainsBlockedIngredients(
        ICollection<int> blockedIngredientIds,
        MenuItem item)
    {
        if (blockedIngredientIds.Count == 0)
        {
            return false;
        }

        ICollection<int> menuItemIngredientIds = item.IngredientIds;

        return blockedIngredientIds.Intersect(menuItemIngredientIds).Any();
    }
}