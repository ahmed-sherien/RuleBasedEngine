namespace RuleBasedEngine.Interfaces
{
    public interface ICanAddStringOperation
    {
        ICanAddConditionOrAction Equal(string value);
        ICanAddConditionOrAction NotEqual(string value);
    }
}