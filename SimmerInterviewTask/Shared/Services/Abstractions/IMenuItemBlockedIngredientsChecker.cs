using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Services.Abstractions;

internal interface IMenuItemBlockedIngredientsChecker
{
    bool ContainsBlockedIngredients(
        ICollection<int> blockedIngredientIds, 
        MenuItem item);
}
