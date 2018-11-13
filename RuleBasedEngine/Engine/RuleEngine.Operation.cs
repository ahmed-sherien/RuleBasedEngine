using RuleBasedEngine.Engine.Interfaces;
using RuleBasedEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Engine
{
    partial class RuleEngine : ICanAddOperation
    {
        public ICanAddConditionOrAction Equal<T>(T value)
        {
            AddCondition<T>(Operation.Equal, value);
            return this;
        }

        public ICanAddConditionOrAction NotEqual<T>(T value)
        {
            AddCondition<T>(Operation.NotEqual, value);
            return this;
        }
    }
}
