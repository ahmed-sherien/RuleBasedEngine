using RuleBasedEngine.Engine.Interfaces;
using RuleBasedEngine.Models;

namespace RuleBasedEngine.Engine
{
    partial class RuleEngine : ICanAddStringOperation
    {
        public ICanAddConditionOrAction Equal(string value)
        {
            AddCondition<string>(Operation.Equal, value);
            return this;
        }

        public ICanAddConditionOrAction NotEqual(string value)
        {
            AddCondition<string>(Operation.NotEqual, value);
            return this;
        }
    }
}
