using System;
using System.Linq.Expressions;
using RuleBasedEngine.Models.Interfaces;

namespace RuleBasedEngine.Interfaces
{
    public interface ICanAddConditionOrAction
    {
        ICanAddIntOperation AndIf<T>(Expression<Func<T, int>> member);
        ICanAddStringOperation AndIf<T>(Expression<Func<T, string>> member);
        ICanAddBoolOperation AndIf<T>(Expression<Func<T, bool>> member);
        ICanAddOperation AndIf<T, M>(Expression<Func<T, M>> member);
        IRule<T> Then<T>(Expression<Func<T, Action>> action);
        IRule<T1, T2> Then<T1, T2>(Expression<Func<T1, Action>> action);
    }
}