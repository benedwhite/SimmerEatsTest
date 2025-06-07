using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared;

internal interface IMenuItemFilter
{
    bool IsAllowed(MenuItem menuItem);
}
