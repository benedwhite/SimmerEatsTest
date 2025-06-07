using SimmerInterviewTask.Shared.Services.Abstractions;

namespace SimmerInterviewTask.Shared.Services;

internal sealed class EvenDistributorCalculator : IEvenDistributorCalculator
{
    public int CalculateMinimumNumber(int count, int itemCount) 
        => count / itemCount;

    public int CalculateLeftOver(int count, int minimumNumber, int itemCount) 
        => count - minimumNumber * itemCount;

    public int CalculateQuantity(int index, int minimumNumber, int leftOver) 
        => minimumNumber + (index < leftOver ? 1 : 0);
}