using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Services;

internal interface IDietRestrictionService
{
    bool AllowedByPreferences(MenuItem menuItem);
}
