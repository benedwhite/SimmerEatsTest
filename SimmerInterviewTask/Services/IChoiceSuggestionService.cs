using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Services;


public interface IChoiceSuggestionService
{
    /// <summary>
    /// Suggests possible choices for a subscription based on the given menu and allocation.
    /// </summary>
    /// <param name="subscriptionContext">Context on the subscription e.g. dietary preferences, reviews, previous orders, etc</param>
    /// <param name="menuContext">The menu context that includes available menu items, recommended items</param>
    /// <param name="allocation">The current choice allocation for the user.</param>
    /// <returns>A collection of suggested choices.</returns>
    public ICollection<EntryChoice> SuggestChoicesFor(
        SubscriptionContext subscriptionContext,
        MenuContext menuContext,
        ChoiceAllocation allocation
    );
}