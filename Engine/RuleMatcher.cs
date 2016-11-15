using RuleBasedEngine.Models;

namespace RuleBasedEngine.Engine
{
    public class RuleMatcher
    {
        private ConditionCompiler _compiler;
        public RuleMatcher()
        {
            _compiler = new ConditionCompiler();
        }
        public MatchResult<T> IsMatch<T>(T item, Rule rule)
        {
            var compiledCondition = _compiler.CompileRule<T>(rule.Condition);
            return new MatchResult<T>
            {
                IsMatch = compiledCondition(item),
                Rule = rule,
                Item = item
            };
        }
    }
}