using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared;

namespace SimmerInterviewTask.Features.Meat;

internal sealed class MeatOnlyMenuItemFilter : IMenuItemFilter
{
    public bool IsAllowed(MenuItem menuItem)
    {
        ArgumentNullException.ThrowIfNull(menuItem);

        if (!menuItem.IsVegetarian && !menuItem.IsVegan)
        {
            return true;
        }

        return menuItem.Type is MenuItemType.Breakfast;
    }
}
