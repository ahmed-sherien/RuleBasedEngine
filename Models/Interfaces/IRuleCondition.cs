using System;
using System.Linq.Expressions;

namespace RuleBasedEngine.Models.Interfaces
{
    public interface IRuleCondition<T>
    {
        Func<T, bool> Compile();
        Expression<Func<T, bool>> GenerateExpression();
    }
}