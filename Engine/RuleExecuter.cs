using System;
using System.Reflection;

public class RuleExecuter
{
    public void ExecuteAction<T>(MatchResult<T> match, string actionName)
    {
        if (!match.IsMatch) return;
        var type = typeof(T);
        var method = type.GetMethod(actionName);
        if (method == null) throw new Exception($"There is no method named {actionName} in the type {type.Name}");
        method.Invoke(match.Item, null);
    }
}