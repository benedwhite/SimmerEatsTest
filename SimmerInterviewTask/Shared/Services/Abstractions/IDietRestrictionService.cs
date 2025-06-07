using SimmerInterviewTask.Model;
using static SimmerInterviewTask.Model.SubscriptionContext;

namespace SimmerInterviewTask.Shared.Services.Abstractions;

internal interface IDietRestrictionService
{
    bool AllowedByPreferences(MenuItem menuItem, DietaryPreferences dietaryPreferences);
}
