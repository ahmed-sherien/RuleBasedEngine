using RuleBasedEngine.Engine.Interfaces;
using RuleBasedEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Engine
{
    partial class RuleEngine : ICanAddDoubleOperation
    {
        public ICanAddConditionOrAction Equal(double value)
        {
            AddCondition<double>(Operation.Equal, value);
            return this;
        }

        public ICanAddConditionOrAction NotEqual(double value)
        {
            AddCondition<double>(Operation.NotEqual, value);
            return this;
        }

        public ICanAddConditionOrAction LessThan(double value)
        {
            AddCondition<double>(Operation.LessThan, value);
            return this;
        }

        public ICanAddConditionOrAction LessThanOrEqual(double value)
        {
            AddCondition<double>(Operation.LessThanOrEqual, value);
            return this;
        }

        public ICanAddConditionOrAction GreaterThan(double value)
        {
            AddCondition<double>(Operation.GreaterThan, value);
            return this;
        }

        public ICanAddConditionOrAction GreaterThanOrEqual(double value)
        {
            AddCondition<double>(Operation.GreaterThanOrEqual, value);
            return this;
        }
    }
}
