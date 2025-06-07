using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Services.Abstractions;

namespace SimmerInterviewTask.Shared.Services;

internal sealed class DictionaryMenuItemTypeCountService : IMenuItemTypeCountService
{
    private readonly Dictionary<MenuItemType, Func<ChoiceAllocation, int>> _countGetters = new()
    {
        [MenuItemType.Main] = allocation => allocation.MainsPermitted,
        [MenuItemType.Breakfast] = allocation => allocation.BreakfastsPermitted
    };

    public int GetPermittedCount(
        MenuItemType menuItemType,
        ChoiceAllocation allocation)
            => _countGetters.TryGetValue(menuItemType, out var getter)
                ? getter(allocation)
                : throw new ArgumentOutOfRangeException(nameof(menuItemType));
}