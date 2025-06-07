using SimmerInterviewTask.Shared.Services.Abstractions;

namespace SimmerInterviewTask.Shared.Services;

internal sealed class RatioSplitter : IRatioSplitter
{
    public (int veganQuantity, int meatQuantity) SplitByRatio(int number, decimal ratio)
    {
        int one = Convert.ToInt32(number * ratio);
        int two = number - one;

        return (one, two);
    }
}