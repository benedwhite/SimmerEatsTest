using SimmerInterviewTask.Core;
using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Features.Meat;

internal sealed class MeatOnlyMenuItemFilter : IMenuItemFilter
{
    public bool IsAllowed(MenuItem menuItem)
    {
        ArgumentNullException.ThrowIfNull(menuItem);

        return (menuItem.IsVegetarian || menuItem.IsVegan)
            && menuItem.Type == MenuItemType.Breakfast;
    }
}
