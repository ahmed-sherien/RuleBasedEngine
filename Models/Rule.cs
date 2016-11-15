using System;
using RuleBasedEngine.Models.Interfaces;

namespace RuleBasedEngine.Models
{
    public class RuleBase<T>
    {
        public IConditionBase<T> Condition { get; set; }
        public string ActionName { get; set; }
        override public string ToString()
        {
            return $"If {Condition}, then {ActionName}";
        }
    }
    public class Rule<T> : RuleBase<Func<T, bool>>
    {
        public Rule(ICondition<T> condition, string actionName)
        {
            Condition = condition;
            ActionName = actionName;
        }
    }
    public class Rule<T1, T2> : RuleBase<Func<T1, T2, bool>>
    {
        public Rule(ICondition<T1, T2> condition, string actionName)
        {
            Condition = condition;
            ActionName = actionName;
        }
    }
}