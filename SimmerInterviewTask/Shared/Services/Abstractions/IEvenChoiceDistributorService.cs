using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Services.Abstractions;

internal interface IEvenChoiceDistributorService
{
    IEnumerable<EntryChoice> DistributeChoicesEvenly(
        ICollection<MenuItem> items, 
        int count, 
        MainPortionSize portionSize);
}