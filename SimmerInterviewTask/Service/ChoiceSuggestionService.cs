using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Service;

public class ChoiceSuggestionService : IChoiceSuggestionService
{
    public ICollection<EntryChoice> SuggestChoicesFor(SubscriptionContext subscriptionContext, MenuContext menuContext, ChoiceAllocation allocation)
    {
        // todo: implement this method anyway you see fit
        // Feel free to lean on the Example as a base which can be refactored and improved upon.
        
        // The objective is to produce a list of choices that the customer is likely to be happy with
        // The SubscriptionContext contains information on reviews a customer has left, items they have ordered in the past
        
        // The example suggestion service ignores most of that data.
        // It just limits the menu to what items a customer is allowed
        // And tries to maintain the proportion of vegan meals that a customer has ordered in the past
        throw new NotImplementedException();
    }
}