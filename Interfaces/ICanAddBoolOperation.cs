namespace RuleBasedEngine.Interfaces
{
    public interface ICanAddBoolOperation
    {
        ICanAddConditionOrAction IsTrue();
        ICanAddConditionOrAction IsFalse();
    }
}