using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Factories.Abstractions;
using SimmerInterviewTask.Shared.Services.Abstractions;

namespace SimmerInterviewTask.Shared.Services;

internal sealed class EvenChoiceDistributorService(
    IRecipeTypeFactory recipeTypeFactory,
    IEvenDistributorCalculator evenDistributorCalculator) : IEvenChoiceDistributorService
{
    private readonly IRecipeTypeFactory _recipeTypeFactory = recipeTypeFactory
        ?? throw new ArgumentNullException(nameof(recipeTypeFactory));

    private readonly IEvenDistributorCalculator _evenDistributorCalculator = evenDistributorCalculator
        ?? throw new ArgumentNullException(nameof(evenDistributorCalculator));

    public IEnumerable<EntryChoice> DistributeChoicesEvenly(
        ICollection<MenuItem> menuItems,
        int count,
        MainPortionSize portionSize)
    {
        ArgumentNullException.ThrowIfNull(menuItems);

        if (menuItems.Count == 0 || count == 0)
        {
            return [];
        }

        int minimumNumber = _evenDistributorCalculator.CalculateMinimumNumber(
            count, 
            menuItems.Count);

        int leftOver = _evenDistributorCalculator.CalculateLeftOver(
            count, 
            minimumNumber, 
            menuItems.Count);

        return menuItems
            .Select(
                (item, index) => new EntryChoice
                {
                    MenuItemId = item.Id,
                    RecipeType = _recipeTypeFactory.CreateFrom(portionSize),
                    Quantity = _evenDistributorCalculator.CalculateQuantity(
                        index,
                        minimumNumber,
                        leftOver),
                    RecipeId = 0,
                })
            .Where(entryChoice => entryChoice.Quantity > 0);
    }
}
