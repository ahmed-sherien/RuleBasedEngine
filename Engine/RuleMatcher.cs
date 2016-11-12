public class RuleMatcher
{
    private RuleCompiler _compiler;
    public RuleMatcher(RuleCompiler compiler)
    {
        _compiler = compiler;
    }
    public MatchResult<T> IsMatch<T>(T item, Rule rule)
    {
        var compiledRule = _compiler.CompileRule<T>(rule);
        return new MatchResult<T>
        {
            IsMatch = compiledRule(item),
            Rule = rule,
            Item = item
        };
    }
}