namespace SimmerInterviewTask.Shared.Services.Abstractions;

internal interface IEvenDistributorCalculator
{
    /** Potentially violates the interface segregation principle, but this is a simple utility class that is unlikely to change often. 
     * If it does, we can refactor it later.
     * This class provides methods to calculate the distribution of items evenly across a given count.
     */

    int CalculateLeftOver(int count, int minimumNumber, int itemCount);

    int CalculateMinimumNumber(int count, int itemCount);

    int CalculateQuantity(int index, int minimumNumber, int leftOver);
}