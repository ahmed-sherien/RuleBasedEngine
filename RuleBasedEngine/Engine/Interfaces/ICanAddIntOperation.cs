using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Engine.Interfaces
{
    public interface ICanAddIntOperation
    {
        ICanAddConditionOrAction Equal(int value);
        ICanAddConditionOrAction NotEqual(int value);
        ICanAddConditionOrAction LessThan(int value);
        ICanAddConditionOrAction LessThanOrEqual(int value);
        ICanAddConditionOrAction GreaterThan(int value);
        ICanAddConditionOrAction GreaterThanOrEqual(int value);
    }
}
