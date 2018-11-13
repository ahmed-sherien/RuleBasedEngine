using RuleBasedEngine.Engine.Interfaces;
using RuleBasedEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Engine
{
    partial class RuleEngine : ICanAddDateTimeOperation
    {
        public ICanAddConditionOrAction Equal(DateTime value)
        {
            AddCondition<DateTime>(Operation.Equal, value);
            return this;
        }

        public ICanAddConditionOrAction NotEqual(DateTime value)
        {
            AddCondition<DateTime>(Operation.NotEqual, value);
            return this;
        }

        public ICanAddConditionOrAction LessThan(DateTime value)
        {
            AddCondition<DateTime>(Operation.LessThan, value);
            return this;
        }

        public ICanAddConditionOrAction LessThanOrEqual(DateTime value)
        {
            AddCondition<DateTime>(Operation.LessThanOrEqual, value);
            return this;
        }

        public ICanAddConditionOrAction GreaterThan(DateTime value)
        {
            AddCondition<DateTime>(Operation.GreaterThan, value);
            return this;
        }

        public ICanAddConditionOrAction GreaterThanOrEqual(DateTime value)
        {
            AddCondition<DateTime>(Operation.GreaterThanOrEqual, value);
            return this;
        }

        public ICanAddConditionOrAction IsToday()
        {
            AddCondition<DateTime>(Operation.GreaterThanOrEqual, DateTime.Today);
            AddCondition<DateTime>(Operation.LessThan, DateTime.Today.AddDays(1));
            return this;
        }
    }
}
