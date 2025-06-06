using SimmerInterviewTask.Core;
using SimmerInterviewTask.Features.Meat;
using SimmerInterviewTask.Features.Vegan;
using SimmerInterviewTask.Features.Vegetarian;
using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Factories;

internal sealed class MenuItemFilterFactory : IMenuItemFilterFactory
{
    public IMenuItemFilter CreateFor(Diet? diet)
        => diet switch
        {
            Diet.Vegan => new VeganMenuItemFilter(),
            Diet.Vegetarian => new VegetarianMenuItemFilter(),
            Diet.MeatOnly => new MeatOnlyMenuItemFilter(),
            _ => new DefaultMenuItemFilter()
        };
}