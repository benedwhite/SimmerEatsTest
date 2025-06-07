using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Services.Abstractions;

internal interface IMenuItemBlockedIngredientsChecker
{
    bool ContainsBlockedIngredients(
        MenuItem item,
        ICollection<int> blockedIngredientIds);
}
