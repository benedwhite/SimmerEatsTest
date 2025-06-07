using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Factories.Abstractions;

internal interface IMenuItemFilterFactory
{
    IMenuItemFilter CreateFrom(Diet? diet);
}