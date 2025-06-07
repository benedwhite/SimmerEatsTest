using SimmerInterviewTask.Features.Meat;
using SimmerInterviewTask.Features.Vegan;
using SimmerInterviewTask.Features.Vegetarian;
using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Factories;

internal sealed class MenuItemFilterFactory(Diet? diet) : IFactory<IMenuItemFilter>
{
    public IMenuItemFilter Create()
        => diet switch
        {
            Diet.Vegan => new VeganMenuItemFilter(),
            Diet.Vegetarian => new VegetarianMenuItemFilter(),
            Diet.MeatOnly => new MeatOnlyMenuItemFilter(),
            _ => new DefaultMenuItemFilter()
        };
}
