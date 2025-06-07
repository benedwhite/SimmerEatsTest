using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Services.Abstractions;

internal interface IDietRestrictionService
{
    bool AllowedByPreferences(MenuItem menuItem);
}
