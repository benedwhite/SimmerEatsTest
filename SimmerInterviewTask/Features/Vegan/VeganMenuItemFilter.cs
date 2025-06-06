using SimmerInterviewTask.Core;
using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Features.Vegan;

internal sealed class VeganMenuItemFilter : IMenuItemFilter
{
    public bool IsAllowed(MenuItem menuItem)
    {
        ArgumentNullException.ThrowIfNull(menuItem);

        return menuItem.IsVegan;
    }
}
