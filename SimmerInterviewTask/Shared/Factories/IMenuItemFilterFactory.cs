using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Factories;

internal interface IMenuItemFilterFactory
{
    IMenuItemFilter CreateFor(Diet? diet);
}
