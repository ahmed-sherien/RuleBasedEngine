using RuleBasedEngine.Engine.Interfaces;
using RuleBasedEngine.Models;
using RuleBasedEngine.Models.Interfaces;
using System;
using System.Linq.Expressions;

namespace RuleBasedEngine.Engine
{
    partial class RuleEngine : ICanAddConditionOrAction
    {
        public ICanAddIntOperation AndIf<T>(Expression<Func<T, int>> member)
        {
            InitiateCondition<T, int>(member);
            return this;
        }

        public ICanAddDateTimeOperation AndIf<T>(Expression<Func<T, DateTime>> member)
        {
            InitiateCondition<T, DateTime>(member);
            return this;
        }

        public ICanAddStringOperation AndIf<T>(Expression<Func<T, string>> member)
        {
            InitiateCondition<T, string>(member);
            return this;
        }

        public ICanAddBoolOperation AndIf<T>(Expression<Func<T, bool>> member)
        {
            InitiateCondition<T, bool>(member);
            return this;
        }

        public ICanAddOperation AndIf<T, M>(Expression<Func<T, M>> member)
        {
            InitiateCondition<T, M>(member);
            return this;
        }

        public IRule<T> Then<T>(Expression<Func<T, Action>> action)
        {
            return new Rule<T>((RuleConditionCollection<T>)_conditionCollection, new RuleAction<T>(action));
        }

        public IRule<T1, T2> Then<T1, T2>(Expression<Func<T1, Action>> action)
        {
            return new Rule<T1, T2>((RuleConditionCollection<T1, T2>)_conditionCollection, new RuleAction<T1>(action));
        }

        public IRule<T> Validate<T>()
        {
            return new Rule<T>((RuleConditionCollection<T>)_conditionCollection);
        }

        public IRule<T1, T2> Validate<T1, T2>()
        {
            return new Rule<T1, T2>((RuleConditionCollection<T1, T2>)_conditionCollection);
        }
    }
}
