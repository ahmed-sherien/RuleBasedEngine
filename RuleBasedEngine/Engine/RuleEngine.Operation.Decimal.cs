using RuleBasedEngine.Engine.Interfaces;
using RuleBasedEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Engine
{
    partial class RuleEngine : ICanAddDecimalOperation
    {
        public ICanAddConditionOrAction Equal(decimal value)
        {
            AddCondition<decimal>(Operation.Equal, value);
            return this;
        }

        public ICanAddConditionOrAction NotEqual(decimal value)
        {
            AddCondition<decimal>(Operation.NotEqual, value);
            return this;
        }

        public ICanAddConditionOrAction LessThan(decimal value)
        {
            AddCondition<decimal>(Operation.LessThan, value);
            return this;
        }

        public ICanAddConditionOrAction LessThanOrEqual(decimal value)
        {
            AddCondition<decimal>(Operation.LessThanOrEqual, value);
            return this;
        }

        public ICanAddConditionOrAction GreaterThan(decimal value)
        {
            AddCondition<decimal>(Operation.GreaterThan, value);
            return this;
        }

        public ICanAddConditionOrAction GreaterThanOrEqual(decimal value)
        {
            AddCondition<decimal>(Operation.GreaterThanOrEqual, value);
            return this;
        }
    }
}
