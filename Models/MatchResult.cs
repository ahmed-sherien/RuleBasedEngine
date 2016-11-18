using RuleBasedEngine.Models.Interfaces;

namespace RuleBasedEngine.Models
{
    public class MatchResult<T> : IMatchResult
    {
        public bool IsMatch { get; set; }
        public T Item { get; set; }
        public IRuleAction<T> Action { get; set; }
        public void Execute()
        {
            if (IsMatch) Action.Method(Item).Invoke();
        }
        public override string ToString()
        {
            return $"{typeof(T).Name} is {(IsMatch ? "" : "not ")}a match";
        }
    }

    public class MatchResult<T1, T2> : MatchResult<T1>
    {
        public T2 ExtraItem1 { get; set; }
    }
}