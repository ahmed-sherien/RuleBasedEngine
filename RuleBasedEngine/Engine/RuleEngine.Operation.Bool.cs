using RuleBasedEngine.Engine.Interfaces;
using RuleBasedEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Engine
{
    partial class RuleEngine : ICanAddBoolOperation
    {
        public ICanAddConditionOrAction IsTrue()
        {
            AddCondition<bool>(Operation.IsTrue);
            return this;
        }

        public ICanAddConditionOrAction IsFalse()
        {
            AddCondition<bool>(Operation.IsFalse);
            return this;
        }
    }
}
