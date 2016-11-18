using System.Collections.Generic;
using System.Linq;
using RuleBasedEngine.Models.Interfaces;

namespace RuleBasedEngine.Models
{
    public class Rule<T> : IRule<T>
    {
        public Rule(List<IRuleCondition<T>> conditions, IRuleAction<T> action)
        {
            Conditions = conditions;
            Action = action;
        }
        public List<IRuleCondition<T>> Conditions { get; set; }
        public IRuleAction<T> Action { get; set; }
        public IMatchResult Match(T item)
        {
            return new MatchResult<T>
            {
                IsMatch = Conditions.All(c => c.Compile().Invoke(item)),
                Item = item,
                Action = Action
            };
        }
        override public string ToString()
        {
            return $"If {Conditions.Select(c => c.ToString()).Aggregate((s1, s2) => s1 + " and " + s2)}, then {Action}";
        }
    }
}