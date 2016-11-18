using System.Collections.Generic;
using System.Linq;
using RuleBasedEngine.Models.Interfaces;

namespace RuleBasedEngine.Models
{
    public class RuleConditionCollection<T>
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
        public virtual bool IsMatch(T item)
        {
            return _conditions.All(c => c.Compile().Invoke(item));
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
            return base.IsMatch(item1) && _conditions.All(c => c.Compile().Invoke(item2));
        }

        override public string ToString()
        {
            return $"{base.ToString()} and {_conditions.Select(c => c.ToString()).Aggregate((s1, s2) => s1 + " and " + s2)}";
        }
    }
}