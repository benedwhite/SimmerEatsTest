using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Core;

internal sealed class DefaultMenuItemFilter : IMenuItemFilter
{
    public bool IsAllowed(MenuItem menuItem) => true;
}
