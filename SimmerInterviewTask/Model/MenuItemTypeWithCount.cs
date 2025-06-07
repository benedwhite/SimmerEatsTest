namespace SimmerInterviewTask.Model;

public sealed record MenuItemTypeWithCount(
    MenuItemType MenuItemType, 
    int PermittedCount);