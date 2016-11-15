using RuleBasedEngine.Models;

namespace RuleBasedEngine.Engine
{
    public class RuleMatcher<T>
    {
        public MatchResult<T> IsMatch(T item, Rule<T> rule)
        {
            var compiledCondition = rule.Condition.Compile();
            return new MatchResult<T>
            {
                IsMatch = compiledCondition(item),
                Rule = rule,
                Item = item
            };
        }
    }
}