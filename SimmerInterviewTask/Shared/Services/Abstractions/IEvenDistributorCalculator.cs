namespace SimmerInterviewTask.Shared.Services.Abstractions;

internal interface IEvenDistributorCalculator
{
    int CalculateLeftOver(int count, int minimumNumber, int itemCount);

    int CalculateMinimumNumber(int count, int itemCount);

    int CalculateQuantity(int index, int minimumNumber, int leftOver);
}