using SimmerInterviewTask.Model;

namespace SimmerInterviewTask.Shared.Factories.Abstractions;

internal interface IRecipeTypeFactory
{
    RecipeType CreateFrom(MainPortionSize mainPortionSize);
}