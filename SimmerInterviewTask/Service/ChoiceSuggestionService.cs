using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Services;

namespace SimmerInterviewTask.Service;

internal sealed class ChoiceSuggestionService(IDietRestrictionService dietRestrictionService) : IChoiceSuggestionService
{
    private readonly IDietRestrictionService _dietRestrictionService = dietRestrictionService
        ?? throw new ArgumentNullException(nameof(dietRestrictionService));

    public ICollection<EntryChoice> SuggestChoicesFor(
        SubscriptionContext subscriptionContext, 
        MenuContext menuContext, 
        ChoiceAllocation allocation)
    {
        // todo: implement this method anyway you see fit
        // Feel free to lean on the Example as a base which can be refactored and improved upon.

        // The objective is to produce a list of choices that the customer is likely to be happy with
        // The SubscriptionContext contains information on reviews a customer has left, items they have ordered in the past

        // The example suggestion service ignores most of that data.
        // It just limits the menu to what items a customer is allowed
        // And tries to maintain the proportion of vegan meals that a customer has ordered in the past

        var allowedItems = menuContext.MenuItems
            .Where(menuItem => _dietRestrictionService.AllowedByPreferences(menuItem))
            .ToList();

        return [];
    }
}
