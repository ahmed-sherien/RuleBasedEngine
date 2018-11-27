using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Engine.Interfaces
{
    public interface ICanAddDecimalOperation
    {
        ICanAddConditionOrAction Equal(decimal value);
        ICanAddConditionOrAction NotEqual(decimal value);
        ICanAddConditionOrAction LessThan(decimal value);
        ICanAddConditionOrAction LessThanOrEqual(decimal value);
        ICanAddConditionOrAction GreaterThan(decimal value);
        ICanAddConditionOrAction GreaterThanOrEqual(decimal value);
    }
}
