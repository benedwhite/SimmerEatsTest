using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Services.Abstractions;

internal interface IMenuItemTypeCountService
{
    int GetPermittedCount(MenuItemType menuItemType, ChoiceAllocation allocation);
}
