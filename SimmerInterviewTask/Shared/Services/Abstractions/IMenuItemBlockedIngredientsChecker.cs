using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Services.Abstractions;

internal interface IMenuItemBlockedIngredientsChecker
{
    bool MenuItemContainsBlockedIngredients(
        ICollection<int> blockedIngredientIds, 
        MenuItem item);
}
