using SimmerInterviewTask.Shared.Factories.Abstractions;
using SimmerInterviewTask.Shared.Services;
using SimmerInterviewTask.Shared.Services.Abstractions;

namespace SimmerInterviewTask.Shared.Factories;

internal class EntryChoicePickerFactory(
    IEvenChoiceDistributorService evenChoiceDistributorService,
    IRatioSplitter ratioSplitter)
    : IEntryChoicePickerFactory
{
    private readonly IEvenChoiceDistributorService _evenChoiceDistributorService = evenChoiceDistributorService
        ?? throw new ArgumentNullException(nameof(evenChoiceDistributorService));

    private readonly IRatioSplitter _ratioSplitter = ratioSplitter
        ?? throw new ArgumentNullException(nameof(ratioSplitter));

    public IEntryChoicePicker CreateFrom(decimal? veganRatio)
        => veganRatio.HasValue
            ? new VeganEntryChoicePicker(
                _evenChoiceDistributorService,
                _ratioSplitter,
                veganRatio.Value)
            : new NonVeganEntryChoicePicker(_evenChoiceDistributorService);
}
