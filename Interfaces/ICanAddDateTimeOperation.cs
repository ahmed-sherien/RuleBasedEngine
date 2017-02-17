using System;

namespace RuleBasedEngine.Interfaces
{
    public interface ICanAddDateTimeOperation
    {
        ICanAddConditionOrAction Equal(DateTime value);
        ICanAddConditionOrAction NotEqual(DateTime value);
        ICanAddConditionOrAction LessThan(DateTime value);
        ICanAddConditionOrAction LessThanOrEqual(DateTime value);
        ICanAddConditionOrAction GreaterThan(DateTime value);
        ICanAddConditionOrAction GreaterThanOrEqual(DateTime value);
        ICanAddConditionOrAction IsToday();
    }
}