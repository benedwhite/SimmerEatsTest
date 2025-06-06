namespace SimmerInterviewTask.Model;

public enum RecipeType
{
    Standard,
    Lean,
    Large
}
    
public enum MainPortionSize
{
    Standard,
    Lean,
    Large
}

    
public enum MenuItemType
{
    Main = 0,
    Breakfast = 2
}
    
public enum Diet
{
    Everything,
    Vegan,
    Vegetarian,
    MeatOnly
}
    
public record EntryChoice
{
    public int MenuItemId { get; set; }
    public int RecipeId { get; set; }
    public RecipeType RecipeType { get; set; }
    public int Quantity { get; set; }
}
    
public class MenuItem
{
    public int Id { get; set; }
    public bool IsVegan { get; set; }
    public bool IsVegetarian { get; set; }
    public MenuItemType Type { get; set; }
    public ICollection<int> IngredientIds { get; set; }
    public bool ExcludeFromSuggestions { get; set; }
}
    
public record MenuContext
{
    public ICollection<MenuItem> MenuItems { get; set; }
    // Business decided, generally bestsellers
    public ICollection<int> RecommendedMenuItemIds { get; set; }
}

public record SubscriptionContext
{
    public ICollection<MenuItemReview> LatestReviews { get; set; }
    public ICollection<PreviouslyOrderedMenuItem> PreviouslyOrderedMenuItems { get; set; }
        
    public decimal? RatioOfExistingChoicesThatAreVegan { get; set; }
    public DietaryPreferences DietPreferences { get; set; }

    public record MenuItemReview
    {
        public int MenuItemId { get; set; }
        // 1-5, 1 being Poor and 5 being Excellent
        public int Score { get; set; }
    }
    public record PreviouslyOrderedMenuItem
    {
        public int MenuItemId { get; set; }
        public int NumberOfOrders { get; set; }
    }
    public record DietaryPreferences
    {
        public Diet? Diet { get; set; }
        public ICollection<int> BlockedIngredientIds { get; set; }
    }
}

public record ChoiceAllocation
{
    public int MainsPermitted { get; set; }
    public int BreakfastsPermitted { get; set; }
    public MainPortionSize MainPortionSize { get; set; }
}