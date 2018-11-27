using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Engine.Interfaces
{
    public interface ICanAddDoubleOperation
    {
        ICanAddConditionOrAction Equal(double value);
        ICanAddConditionOrAction NotEqual(double value);
        ICanAddConditionOrAction LessThan(double value);
        ICanAddConditionOrAction LessThanOrEqual(double value);
        ICanAddConditionOrAction GreaterThan(double value);
        ICanAddConditionOrAction GreaterThanOrEqual(double value);
    }
}
