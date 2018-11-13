using RuleBasedEngine.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RuleBasedEngine.Models
{
    public class RuleConditionCollection<T> : IRuleConditionCollection
    {
        private List<IRuleCondition<T>> _conditions;
        public RuleConditionCollection()
        {
            _conditions = new List<IRuleCondition<T>>();
        }

        public void Add(IRuleCondition<T> condition)
        {
            _conditions.Add(condition);
        }

        public bool IsMatch(T item)
        {
            return _conditions.All(c => c.IsMatch(item));
        }

        override public string ToString()
        {
            return $"{_conditions.Select(c => c.ToString()).Aggregate((s1, s2) => s1 + " and " + s2)}";
        }
    }

    public class RuleConditionCollection<T1, T2> : RuleConditionCollection<T1>
    {
        private List<IRuleCondition<T2>> _conditions;
        public RuleConditionCollection() : base()
        {
            _conditions = new List<IRuleCondition<T2>>();
        }

        public void Add(IRuleCondition<T2> condition)
        {
            _conditions.Add(condition);
        }

        public bool IsMatch(T1 item1, T2 item2)
        {
            return Item1IsMatch(item1) && Item2IsMatch(item2);
        }

        public bool Item1IsMatch(T1 item1)
        {
            return base.IsMatch(item1);
        }

        public bool Item2IsMatch(T2 item2)
        {
            return _conditions.All(c => c.IsMatch(item2));
        }

        override public string ToString()
        {
            return $"{base.ToString()} and {_conditions.Select(c => c.ToString()).Aggregate((s1, s2) => s1 + " and " + s2)}";
        }
    }
}
