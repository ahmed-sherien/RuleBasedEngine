namespace RuleBasedEngine.Interfaces
{
    public interface ICanAddOperation
    {
        ICanAddConditionOrAction Equal<T>(T value);
        ICanAddConditionOrAction NotEqual<T>(T value);
    }
}