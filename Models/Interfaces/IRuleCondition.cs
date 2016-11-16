using System;

namespace RuleBasedEngine.Models.Interfaces
{
    public interface IRuleCondition<T> : IRuleConditionBase<Func<T, bool>>
    {
        
    }
}