using RuleBasedEngine.Engine.Interfaces;
using RuleBasedEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Engine
{
    partial class RuleEngine : ICanAddIntOperation
    {
        public ICanAddConditionOrAction Equal(int value)
        {
            AddCondition<int>(Operation.Equal, value);
            return this;
        }

        public ICanAddConditionOrAction NotEqual(int value)
        {
            AddCondition<int>(Operation.NotEqual, value);
            return this;
        }

        public ICanAddConditionOrAction LessThan(int value)
        {
            AddCondition<int>(Operation.LessThan, value);
            return this;
        }

        public ICanAddConditionOrAction LessThanOrEqual(int value)
        {
            AddCondition<int>(Operation.LessThanOrEqual, value);
            return this;
        }

        public ICanAddConditionOrAction GreaterThan(int value)
        {
            AddCondition<int>(Operation.GreaterThan, value);
            return this;
        }

        public ICanAddConditionOrAction GreaterThanOrEqual(int value)
        {
            AddCondition<int>(Operation.GreaterThanOrEqual, value);
            return this;
        }
    }
}
