using RuleBasedEngine.Engine.Interfaces;
using RuleBasedEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Engine
{
    partial class RuleEngine : ICanAddFloatOperation
    {
        public ICanAddConditionOrAction Equal(float value)
        {
            AddCondition<float>(Operation.Equal, value);
            return this;
        }

        public ICanAddConditionOrAction NotEqual(float value)
        {
            AddCondition<float>(Operation.NotEqual, value);
            return this;
        }

        public ICanAddConditionOrAction LessThan(float value)
        {
            AddCondition<float>(Operation.LessThan, value);
            return this;
        }

        public ICanAddConditionOrAction LessThanOrEqual(float value)
        {
            AddCondition<float>(Operation.LessThanOrEqual, value);
            return this;
        }

        public ICanAddConditionOrAction GreaterThan(float value)
        {
            AddCondition<float>(Operation.GreaterThan, value);
            return this;
        }

        public ICanAddConditionOrAction GreaterThanOrEqual(float value)
        {
            AddCondition<float>(Operation.GreaterThanOrEqual, value);
            return this;
        }
    }
}
