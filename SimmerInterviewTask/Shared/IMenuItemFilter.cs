using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Core;

internal interface IMenuItemFilter
{
    bool IsAllowed(MenuItem menuItem);
}
