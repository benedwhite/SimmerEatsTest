using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Services.Abstractions;

internal interface IEntryChoicePicker
{
    IEnumerable<EntryChoice> GetChoicesFrom(ICollection<MenuItem> items, int count, MainPortionSize portionSize);
}
