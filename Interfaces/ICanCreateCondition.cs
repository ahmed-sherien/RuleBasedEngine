using System;
using System.Linq.Expressions;

namespace RuleBasedEngine.Interfaces
{
    public interface ICanCreateCondition
    {
        ICanAddIntOperation If<T>(Expression<Func<T, int>> member);
        ICanAddDateTimeOperation If<T>(Expression<Func<T, DateTime>> member);
        ICanAddStringOperation If<T>(Expression<Func<T, string>> member);
        ICanAddBoolOperation If<T>(Expression<Func<T, bool>> member);
        ICanAddOperation If<T, M>(Expression<Func<T, M>> member);
    }
}