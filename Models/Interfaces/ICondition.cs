using System;

namespace RuleBasedEngine.Models.Interfaces
{
    public interface IConditionBase<T> : ICompilable<T>
    {
    }
    public interface ICondition<T> : IConditionBase<Func<T, bool>>
    {
    }
    public interface ICondition<T1, T2> : IConditionBase<Func<T1, T2, bool>>
    {
    }
}