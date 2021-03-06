﻿using RuleBasedEngine.Models.Interfaces;

namespace RuleBasedEngine.Models
{
    public class Rule<T1, T2> : IRule<T1, T2>
    {
        public Rule(RuleConditionCollection<T1, T2> conditions)
        {
            Conditions = conditions;
        }
        public Rule(RuleConditionCollection<T1, T2> conditions, IRuleAction<T1> action) : this(conditions)
        {
            Action = action;
        }
        public RuleConditionCollection<T1, T2> Conditions { get; set; }
        public IRuleAction<T1> Action { get; set; }

        public IMatchResult Match(T1 item1, T2 item2)
        {
            return new MatchResult<T1, T2>
            {
                IsMatch = Conditions.IsMatch(item1, item2),
                Item1IsMatch = Conditions.Item1IsMatch(item1),
                Item2IsMatch = Conditions.Item2IsMatch(item2),
                Item = item1,
                ExtraItem = item2,
                Action = Action
            };
        }

        override public string ToString()
        {
            return $"If {Conditions}, then {(Action != null ? Action.ToString() : "validate")}";
        }
    }
}
