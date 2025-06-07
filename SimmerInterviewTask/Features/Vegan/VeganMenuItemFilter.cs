using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared;

namespace SimmerInterviewTask.Features.Vegan;

internal sealed class VeganMenuItemFilter : IMenuItemFilter
{
    public bool IsAllowed(MenuItem menuItem)
    {
        ArgumentNullException.ThrowIfNull(menuItem);

        return menuItem.IsVegan;
    }
}
