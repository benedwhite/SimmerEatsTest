namespace SimmerInterviewTask.Shared.Factories;

internal interface IFactory<T>
{
    T Create();
}
