using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Factories;

internal interface IMenuItemFilterFactory
{
    IMenuItemFilter CreateFrom(Diet? diet);
}
