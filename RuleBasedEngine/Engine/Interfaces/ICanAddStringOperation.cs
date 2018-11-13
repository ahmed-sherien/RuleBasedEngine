using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Engine.Interfaces
{
    public interface ICanAddStringOperation
    {
        ICanAddConditionOrAction Equal(string value);
        ICanAddConditionOrAction NotEqual(string value);
    }
}
