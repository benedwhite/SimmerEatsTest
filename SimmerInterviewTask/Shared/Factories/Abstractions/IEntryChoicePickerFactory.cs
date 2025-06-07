using SimmerInterviewTask.Shared.Services.Abstractions;

namespace SimmerInterviewTask.Shared.Factories.Abstractions;

internal interface IEntryChoicePickerFactory
{
    IEntryChoicePicker CreateFrom(decimal? veganRatio);
}
