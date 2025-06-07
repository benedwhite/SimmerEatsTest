using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared;

namespace SimmerInterviewTask.Features.Vegetarian;

internal sealed class VegetarianMenuItemFilter : IMenuItemFilter
{
    public bool IsAllowed(MenuItem menuItem)
    {
        ArgumentNullException.ThrowIfNull(menuItem);

        return menuItem.IsVegetarian;
    }
}
