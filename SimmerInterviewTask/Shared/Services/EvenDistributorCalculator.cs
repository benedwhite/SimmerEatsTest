using SimmerInterviewTask.Shared.Services.Abstractions;

namespace SimmerInterviewTask.Shared.Services;

internal sealed class EvenDistributorCalculator : IEvenDistributorCalculator
{
    /** Potentially violates the interface segregation principle, but this is a simple utility class that is unlikely to change often. 
     * If it does, we can refactor it later.
     * This class provides methods to calculate the distribution of items evenly across a given count.
     */

    public int CalculateMinimumNumber(int count, int itemCount) 
        => count / itemCount;

    public int CalculateLeftOver(int count, int minimumNumber, int itemCount) 
        => count - minimumNumber * itemCount;

    public int CalculateQuantity(int index, int minimumNumber, int leftOver) 
        => minimumNumber + (index < leftOver ? 1 : 0);
}