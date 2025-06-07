namespace Simmer.Tests;

using SimmerInterviewTask.Model;
using System.Collections.Generic;

internal static class TestDataHelper
{
    internal static MenuItem CreateMenuItem(
        int id = 1,
        bool isVegan = false,
        bool isVegetarian = false,
        MenuItemType type = MenuItemType.Main,
        IEnumerable<int>? ingredientIds = null)
    {
        return new MenuItem
        {
            Id = id,
            IsVegan = isVegan,
            IsVegetarian = isVegetarian,
            Type = type,
            IngredientIds = ingredientIds?.ToList() ?? [1, 2, 3]
        };
    }

    internal static SubscriptionContext.DietaryPreferences CreateDietaryPreferences(
        Diet? diet = Diet.MeatOnly,
        IEnumerable<int>? blockedIngredientIds = null)
    {
        return new SubscriptionContext.DietaryPreferences
        {
            Diet = diet,
            BlockedIngredientIds = blockedIngredientIds?.ToList() ?? []
        };
    }

    internal static MenuContext CreateMenuContext(MenuItem menuItem)
    {
        return new MenuContext
        {
            MenuItems = [menuItem],
            RecommendedMenuItemIds = [menuItem.Id]
        };
    }

    internal static SubscriptionContext CreateSubscriptionContext(
        SubscriptionContext.DietaryPreferences? dietaryPreferences = null)
    {
        return new SubscriptionContext
        {
            LatestReviews = [],
            DietPreferences = dietaryPreferences ?? CreateDietaryPreferences(),
            PreviouslyOrderedMenuItems = []
        };
    }

    internal static ChoiceAllocation CreateChoiceAllocation(
        int mainsPermitted = 1,
        int breakfastsPermitted = 1,
        MainPortionSize portionSize = MainPortionSize.Standard)
    {
        return new ChoiceAllocation
        {
            MainsPermitted = mainsPermitted,
            BreakfastsPermitted = breakfastsPermitted,
            MainPortionSize = portionSize
        };
    }
}