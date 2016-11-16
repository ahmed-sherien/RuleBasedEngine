namespace RuleBasedEngine.Models.Interfaces
{
    public interface IRule<T>
    {
        IMatchResult Match(T item);
    }
}