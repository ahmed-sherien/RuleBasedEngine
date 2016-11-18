using System;
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

    public class Rule<T1, T2> : IRule<T1, T2>
    {
        public Rule(List<IRuleCondition<T1>> mainConditions, List<IRuleCondition<T2>> extraConditions, IRuleAction<T1> action)
        {
            MainConditions = mainConditions;
            ExtraConditions = extraConditions;
            Action = action;
        }
        
        public List<IRuleCondition<T1>> MainConditions { get; set; }
        public List<IRuleCondition<T2>> ExtraConditions { get; set; }
        public IRuleAction<T1> Action { get; set; }

        public IMatchResult Match(T1 item1, T2 item2)
        {
            return new MatchResult<T1>
            {
                IsMatch = MainConditions.All(c => c.Compile().Invoke(item1))  && ExtraConditions.All(c => c.Compile().Invoke(item2)),
                Item = item1,
                Action = Action
            };
        }
        
        override public string ToString()
        {
            var conditions = $"{MainConditions.Select(c => c.ToString()).Aggregate((s1, s2) => s1 + " and " + s2)}";
            var extraConditions = $"{ExtraConditions.Select(c => c.ToString()).Aggregate((s1, s2) => s1 + " and " + s2)}";
            return $"If {conditions} and {extraConditions}, then {Action}";
        }
    }
}