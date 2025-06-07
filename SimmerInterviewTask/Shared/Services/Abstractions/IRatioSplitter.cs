namespace SimmerInterviewTask.Shared.Services.Abstractions;

internal interface IRatioSplitter
{
    (int veganQuantity, int meatQuantity) SplitByRatio(int number, decimal ratio);
}