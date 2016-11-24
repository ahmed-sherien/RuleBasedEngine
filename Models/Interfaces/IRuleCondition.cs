using System;
using System.Linq.Expressions;

namespace RuleBasedEngine.Models.Interfaces
{
    public interface IRuleCondition
    {
        
    }
    public interface IRuleCondition<T> : IRuleCondition
    {
        Func<T, bool> Compile();
        Expression<Func<T, bool>> GenerateExpression();
    }

    public interface IRuleCondition<T1, T2> : IRuleCondition
    {
        Func<T1, T2, bool> Compile();
        Expression<Func<T1, T2, bool>> GenerateExpression();
    }
}