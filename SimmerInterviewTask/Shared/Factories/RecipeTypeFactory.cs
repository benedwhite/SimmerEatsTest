using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Factories;

internal sealed class RecipeTypeFactory(MainPortionSize mainPortionSize) : IFactory<RecipeType>
{
    public RecipeType Create()
        => mainPortionSize switch
        {
            MainPortionSize.Large => RecipeType.Large,
            _ => RecipeType.Standard
        };
}