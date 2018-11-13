using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Engine.Interfaces
{
    public interface ICanAddBoolOperation
    {
        ICanAddConditionOrAction IsTrue();
        ICanAddConditionOrAction IsFalse();
    }
}
