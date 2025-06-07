using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared;

internal sealed class DefaultMenuItemFilter : IMenuItemFilter
{
    public bool IsAllowed(MenuItem menuItem) => true;
}
