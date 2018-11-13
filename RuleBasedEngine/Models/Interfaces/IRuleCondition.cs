using System;
using System.Linq.Expressions;

namespace RuleBasedEngine.Models.Interfaces
{
    public interface IRuleCondition
    {

    }

    public interface IRuleCondition<T> : IRuleCondition
    {
        bool IsMatch(T member);
        Expression<Func<T, bool>> GenerateExpression();
    }

    public interface IRuleCondition<T1, T2> : IRuleCondition
    {
        bool IsMatch(T1 member1, T2 member2);
        Expression<Func<T1, T2, bool>> GenerateExpression();
    }
}
