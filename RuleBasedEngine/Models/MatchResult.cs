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
            if (IsMatch && Action != null)
            {
                Action.Method(Item).Invoke();
            }
        }

        public override string ToString()
        {
            return $"{typeof(T).Name} is {(IsMatch ? "" : "not ")}a match";
        }
    }

    public class MatchResult<T1, T2> : MatchResult<T1>
    {
        public T2 ExtraItem { get; set; }
        public bool Item1IsMatch { get; set; }
        public bool Item2IsMatch { get; set; }

        public override string ToString()
        {
            return $"{typeof(T1).Name} is {(Item1IsMatch ? "" : "not ")}a match and {typeof(T2).Name} is {(Item2IsMatch ? "" : "not ")}a match";
        }
    }
}
