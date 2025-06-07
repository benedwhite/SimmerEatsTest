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
        ICollection<MenuItem> items,
        int count,
        MainPortionSize portionSize)
    {
        ArgumentNullException.ThrowIfNull(items);

        if (items.Count == 0 || count == 0)
        {
            return [];
        }

        int minimumNumber = _evenDistributorCalculator.CalculateMinimumNumber(count, items.Count);
        int leftOver = _evenDistributorCalculator.CalculateLeftOver(count, minimumNumber, items.Count);

        return items
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
