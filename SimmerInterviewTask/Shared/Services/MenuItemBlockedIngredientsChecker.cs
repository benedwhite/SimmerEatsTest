using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Services.Abstractions;

namespace SimmerInterviewTask.Shared.Services;

internal sealed class MenuItemBlockedIngredientsChecker : IMenuItemBlockedIngredientsChecker
{
    public bool ContainsBlockedIngredients(
        MenuItem item,
        ICollection<int> blockedIngredientIds)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (blockedIngredientIds is null 
            || blockedIngredientIds.Count == 0)
        {
            return false;
        }

        return blockedIngredientIds
            .Intersect(item.IngredientIds)
            .Any();
    }
}