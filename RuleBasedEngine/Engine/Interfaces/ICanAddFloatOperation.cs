using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Engine.Interfaces
{
    public interface ICanAddFloatOperation
    {
        ICanAddConditionOrAction Equal(float value);
        ICanAddConditionOrAction NotEqual(float value);
        ICanAddConditionOrAction LessThan(float value);
        ICanAddConditionOrAction LessThanOrEqual(float value);
        ICanAddConditionOrAction GreaterThan(float value);
        ICanAddConditionOrAction GreaterThanOrEqual(float value);
    }
}
