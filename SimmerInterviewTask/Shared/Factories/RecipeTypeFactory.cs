using SimmerInterviewTask.Model;
using SimmerInterviewTask.Shared.Factories.Abstractions;

namespace SimmerInterviewTask.Shared.Factories;

internal sealed class RecipeTypeFactory : IRecipeTypeFactory
{
    public RecipeType CreateFrom(MainPortionSize mainPortionSize)
        => mainPortionSize switch
        {
            MainPortionSize.Large => RecipeType.Large,
            _ => RecipeType.Standard
        };
}