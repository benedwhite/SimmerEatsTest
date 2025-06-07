using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Services.Abstractions;

namespace SimmerInterviewTask.Shared.Services;

internal sealed class NonVeganEntryChoicePicker(
    IEvenChoiceDistributorService evenChoiceDistributorService) : IEntryChoicePicker
{
    private readonly IEvenChoiceDistributorService _evenChoiceDistributorService = evenChoiceDistributorService
        ?? throw new ArgumentNullException(nameof(evenChoiceDistributorService));

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

        return _evenChoiceDistributorService.DistributeChoicesEvenly(
            items,
            count,
            portionSize);
    }
}
