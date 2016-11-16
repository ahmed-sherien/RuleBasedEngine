using System;
using RuleBasedEngine.Models.Interfaces;

namespace RuleBasedEngine.Models
{
    public class Rule<T> : IRule<T>
    {
        public Rule(IRuleCondition<T> condition, IRuleAction<T> action)
        {
            Condition = condition;
            Action = action;
        }
        public IRuleCondition<T> Condition { get; set; }
        public IRuleAction<T> Action { get; set; }
        public IMatchResult Match(T item)
        {
            var compiledCondition = Condition.Compile();
            return new MatchResult<T>
            {
                IsMatch = compiledCondition(item),
                Item = item,
                Action = Action
            };
        }
        override public string ToString()
        {
            return $"If {Condition}, then {Action}";
        }
    }
}