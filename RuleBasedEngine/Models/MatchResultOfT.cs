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
}
