using RuleBasedEngine.Models.Interfaces;

namespace RuleBasedEngine.Models
{
    public class Rule<T> : IRule<T>
    {
        public Rule(RuleConditionCollection<T> conditions)
        {
            Conditions = conditions;
        }
        public Rule(RuleConditionCollection<T> conditions, IRuleAction<T> action) : this(conditions)
        {
            Action = action;
        }
        public RuleConditionCollection<T> Conditions { get; set; }
        public IRuleAction<T> Action { get; set; }
        public IMatchResult Match(T item)
        {
            return new MatchResult<T>
            {
                IsMatch = Conditions.IsMatch(item),
                Item = item,
                Action = Action
            };
        }
        override public string ToString()
        {
            return $"If {Conditions}, then {(Action != null ? Action.ToString() : "validate")}";
        }
    }
}
