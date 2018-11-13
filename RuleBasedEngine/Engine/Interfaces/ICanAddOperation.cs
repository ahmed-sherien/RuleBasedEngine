using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Engine.Interfaces
{
    public interface ICanAddOperation
    {
        ICanAddConditionOrAction Equal<T>(T value);
        ICanAddConditionOrAction NotEqual<T>(T value);
    }
}
