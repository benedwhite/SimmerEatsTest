namespace SimmerInterviewTask.Shared.Services.Abstractions;

internal interface IEvenDistributorCalculator
{
    /** Potentially violates the interface segregation principle, but this is a simple utility class that is unlikely to change often. 
     * Given time constraints and the nature of the task, it will remain as is.
     */

    int CalculateLeftOver(int count, int minimumNumber, int itemCount);

    int CalculateMinimumNumber(int count, int itemCount);

    int CalculateQuantity(int index, int minimumNumber, int leftOver);
}