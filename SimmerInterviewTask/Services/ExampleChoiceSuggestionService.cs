using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Services;

public class ExampleChoiceSuggestionService : IChoiceSuggestionService
{
    public ICollection<EntryChoice> SuggestChoicesFor(
        SubscriptionContext subscriptionContext, 
        MenuContext menuContext, 
        ChoiceAllocation allocation)
    {
        var allowedItems = menuContext.MenuItems
            .Where(item => AllowedByPreferences(subscriptionContext, item))
            .ToList();
        
        var veganRatio = subscriptionContext.RatioOfExistingChoicesThatAreVegan;
        
        var mainsPermitted = allocation.MainsPermitted;
        var breakfastsPermitted = allocation.BreakfastsPermitted;
        var portionSize = allocation.MainPortionSize;

        var mains = GetChoicesFrom(
            allowedItems.Where(item => item.Type == MenuItemType.Main).ToList(),
            mainsPermitted,
            portionSize,
            veganRatio
        );

        if (breakfastsPermitted == 0)
            return mains;

        var breakfasts = GetChoicesFrom(
            allowedItems.Where(item => item.Type == MenuItemType.Breakfast).ToList(),
            breakfastsPermitted,
            portionSize,
            veganRatio
        );

        return mains.Concat(breakfasts).ToList();
    }
    
    private ICollection<EntryChoice> GetChoicesFrom(
        ICollection<MenuItem> items,
        int count,
        MainPortionSize portionSize,
        decimal? veganRatio
    )
    {
        if (veganRatio is null)
        {
            return SuggestEvenlyDistributedChoiceFor(items, count, portionSize);
        }

        var (veganQuantity, meatQuantity) = SplitByRatio(count, veganRatio.Value);

        var itemsByIsVegan = items.ToLookup(x => x.IsVegan);

        var veganItems = itemsByIsVegan[true].ToList();
        var meatItems = itemsByIsVegan[false].ToList();

        var willVeganWork = veganQuantity == 0 || veganItems.Count > 0;
        var willMeatWork = meatQuantity == 0 || meatItems.Count > 0;

        if (willVeganWork && willMeatWork)
        {
            return SuggestEvenlyDistributedChoiceFor(veganItems, veganQuantity, portionSize)
                .Concat(SuggestEvenlyDistributedChoiceFor(meatItems, meatQuantity, portionSize))
                .ToList();
        }

        return SuggestEvenlyDistributedChoiceFor(items, count, portionSize);
    }
    
    public ICollection<EntryChoice> SuggestEvenlyDistributedChoiceFor(
        ICollection<MenuItem> items,
        int count,
        MainPortionSize portionSize
    )
    {
        if (items.Count == 0 && count > 0)
        {
            return Array.Empty<EntryChoice>();
        }
            
        if (items.Count == 0 || count == 0)
            return Array.Empty<EntryChoice>();

        // this splits the count as evenly as possible across the allowed items
        var minimumNumber = count / items.Count;
        var leftOver = count - minimumNumber * items.Count;
        
        var choices = items
            .Select(
                (item, index) => new EntryChoice
                {
                    MenuItemId = item.Id,
                    RecipeType = RecipeTypeFor(portionSize),
                    Quantity = minimumNumber + (index < leftOver ? 1 : 0),
                    RecipeId = 0,
                })
            .Where(c => c.Quantity > 0)
            .ToList();

        return choices;

        static RecipeType RecipeTypeFor(MainPortionSize portionSize) =>
            portionSize switch
            {
                MainPortionSize.Large => RecipeType.Large,
                _ => RecipeType.Standard
            };
    }
    
    private static bool AllowedByPreferences(SubscriptionContext context, MenuItem item)
    {
        var isAllowedByDiet = AllowedByDiet(context.DietPreferences.Diet, item);

        if (!isAllowedByDiet)
            return false;

        var itemsContainsBlockedIngredients = ContainsBlockedIngredient(context, item);

        return !itemsContainsBlockedIngredients;
    }

    private static bool AllowedByDiet(Diet? diet, MenuItem item)
    {
        if (diet is null)
            return true;

        if (diet == Diet.Vegan && !item.IsVegan)
            return false;

        if (diet == Diet.Vegetarian && !item.IsVegetarian)
            return false;

        if (diet == Diet.MeatOnly && (item.IsVegetarian || item.IsVegan))
        {
            // Generally meat-only customers get only non-vegetarian dishes
            // but breakfasts are an exception
            // We will allow meat-only customers to have vegan breakfasts (e.g. oats)

            var isBreakfast = item.Type == MenuItemType.Breakfast;

            if (!isBreakfast)
                return false;
        }

        return true;
    }
    
    private static (int, int) SplitByRatio(int number, decimal ratio)
    {
        // ensures that the two parts sum to the original number
        var one = Convert.ToInt32(number * ratio);
        var two = number - one;
        return (one, two);
    }


    private static bool ContainsBlockedIngredient(SubscriptionContext context, MenuItem item)
    {
        if (context.DietPreferences.BlockedIngredientIds.Count == 0)
            return false;

        var itemIngredientIds = item.IngredientIds;

        return context.DietPreferences.BlockedIngredientIds.Intersect(itemIngredientIds).Any();
    }
}