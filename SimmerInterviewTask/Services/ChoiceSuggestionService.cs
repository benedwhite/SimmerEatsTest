using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Factories.Abstractions;
using SimmerInterviewTask.Shared.Services.Abstractions;

namespace SimmerInterviewTask.Services;

internal sealed class ChoiceSuggestionService(
    IDietRestrictionService dietRestrictionService,
    IEntryChoicePickerFactory entryChoicePickerFactory,
    IMenuItemTypeCountService menuItemTypeCountProvider,
    IEnumerable<MenuItemType> menuItemTypes) : IChoiceSuggestionService
{
    private readonly IDietRestrictionService _dietRestrictionService = dietRestrictionService
        ?? throw new ArgumentNullException(nameof(dietRestrictionService));

    private readonly IEntryChoicePickerFactory _entryChoicePickerFactory = entryChoicePickerFactory
        ?? throw new ArgumentNullException(nameof(entryChoicePickerFactory));

    private readonly IMenuItemTypeCountService _menuItemTypeCountProvider = menuItemTypeCountProvider
        ?? throw new ArgumentNullException(nameof(menuItemTypeCountProvider));

    private readonly IEnumerable<MenuItemType> _menuItemTypes = menuItemTypes
        ?? throw new ArgumentNullException(nameof(menuItemTypes));

    public ICollection<EntryChoice> SuggestChoicesFor(
        SubscriptionContext subscriptionContext,
        MenuContext menuContext,
        ChoiceAllocation allocation)
    {
        ArgumentNullException.ThrowIfNull(subscriptionContext);
        ArgumentNullException.ThrowIfNull(menuContext);
        ArgumentNullException.ThrowIfNull(allocation);

        ICollection<MenuItem> itemsMatchingDiet = [.. menuContext.MenuItems.Where(
            menuItem => _dietRestrictionService.AllowedByPreferences(
                menuItem,
                subscriptionContext.DietPreferences))];

        decimal? veganRatio = subscriptionContext.RatioOfExistingChoicesThatAreVegan;

        IEntryChoicePicker choicePicker = _entryChoicePickerFactory.CreateFrom(veganRatio);

        return [..
            _menuItemTypes
                .Select(menuItemType => new MenuItemTypeWithCount(
                    menuItemType, 
                    PermittedCount: _menuItemTypeCountProvider.GetPermittedCount(menuItemType, allocation)))
                .Where(menuItemTypeWithCount => menuItemTypeWithCount.PermittedCount > 0)
                .SelectMany(menuItemTypeWithCount =>
                    choicePicker.GetChoicesFrom(
                        itemsMatchingDiet,
                        menuItemTypeWithCount.PermittedCount,
                        allocation.MainPortionSize))];
    }
}
