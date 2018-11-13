namespace RuleBasedEngine.Models.Interfaces
{
    public interface IRule<T>
    {
        IMatchResult Match(T item);
    }

    public interface IRule<T1, T2>
    {
        IMatchResult Match(T1 item1, T2 item2);
    }
}
