using System;
using System.Linq.Expressions;

namespace RuleBasedEngine.Models.Interfaces
{
    public interface IRuleCondition<T>
    {
        Func<T, bool> Compile();
        Expression<Func<T, bool>> GenerateExpression();
    }

    public interface IRuleCondition<T1, T2>
    {
        Func<T1, T2, bool> Compile();
        Expression<Func<T1, T2, bool>> GenerateExpression();
    }
}