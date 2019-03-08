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
}
