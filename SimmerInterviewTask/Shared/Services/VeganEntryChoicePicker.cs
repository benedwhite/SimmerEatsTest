using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Services.Abstractions;

namespace SimmerInterviewTask.Shared.Services;

internal sealed class VeganEntryChoicePicker(
    IEvenChoiceDistributorService evenChoiceDistributorService,
    IRatioSplitter ratioSplitter,
    decimal veganRatio) : IEntryChoicePicker
{
    private readonly IEvenChoiceDistributorService _evenChoiceDistributorService = evenChoiceDistributorService
        ?? throw new ArgumentNullException(nameof(evenChoiceDistributorService));

    private readonly IRatioSplitter _ratioSplitter = ratioSplitter
        ?? throw new ArgumentNullException(nameof(ratioSplitter));

    public IEnumerable<EntryChoice> GetChoicesFrom(
        ICollection<MenuItem> items,
        int count,
        MainPortionSize portionSize)
    {
        ArgumentNullException.ThrowIfNull(items);

        // If time permitted, I would implement a validator here to comply with the DRY principle across strategies
        if (items.Count is 0 || count is 0)
        {
            return [];
        }

        var (veganQuantity, meatQuantity) = _ratioSplitter.SplitByRatio(count, veganRatio);

        ILookup<bool, MenuItem> itemsByIsVegan = items.ToLookup(x => x.IsVegan);

        var veganItems = itemsByIsVegan[true].ToList();
        var meatItems = itemsByIsVegan[false].ToList();

        bool willVeganWork = WillQuantityWork(veganQuantity, veganItems);
        bool willMeatWork = WillQuantityWork(meatQuantity, meatItems);

        if (willVeganWork && willMeatWork)
        {
            return
            [
                .. _evenChoiceDistributorService.DistributeChoicesEvenly(veganItems, veganQuantity, portionSize),
                .. _evenChoiceDistributorService.DistributeChoicesEvenly(meatItems, meatQuantity, portionSize)
            ];
        }

        return _evenChoiceDistributorService.DistributeChoicesEvenly(items, count, portionSize);
    }

    private static bool WillQuantityWork(int quantity, List<MenuItem> items)
        => quantity == 0 || items.Count > 0;
}
