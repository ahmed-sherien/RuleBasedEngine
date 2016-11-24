using System;
using System.Linq.Expressions;
using System.Reflection;
using RuleBasedEngine.Interfaces;
using RuleBasedEngine.Models;
using RuleBasedEngine.Models.Interfaces;

namespace RuleBasedEngine
{
    public class RuleEngine : ICanCreateCondition, ICanAddOperation, ICanAddStringOperation, ICanAddIntOperation, ICanAddBoolOperation, ICanAddConditionOrAction
    {
        private RuleEngine(IRuleConditionCollection conditionCollection, params Type[] types)
        {
            _conditionCollection = conditionCollection;
            _types = types;
        }
        #region Fields
        private Type[] _types;
        private IRuleConditionCollection _conditionCollection;
        private Expression _member;
        private Type _memberType;
        #endregion
        #region Initiating
        public static ICanCreateCondition CreateRule<T>()
        {
            return new RuleEngine(new RuleConditionCollection<T>(), typeof(T));
        }
        public static ICanCreateCondition CreateRule<T1, T2>()
        {
            return new RuleEngine(new RuleConditionCollection<T1, T2>(), typeof(T1), typeof(T2));
        }
        #endregion
        #region Conditions    
        public ICanAddStringOperation If<T>(Expression<Func<T, string>> member)
        {
            InitiateCondition<T, string>(member);
            return this;
        }
        public ICanAddIntOperation If<T>(Expression<Func<T, int>> member)
        {
            InitiateCondition<T, int>(member);
            return this;
        }
        public ICanAddBoolOperation If<T>(Expression<Func<T, bool>> member)
        {
            InitiateCondition<T, bool>(member);
            return this;
        }
        public ICanAddOperation If<T, M>(Expression<Func<T, M>> member)
        {
            InitiateCondition<T, M>(member);
            return this;
        }
        public ICanAddIntOperation AndIf<T>(Expression<Func<T, int>> member)
        {
            InitiateCondition<T, int>(member);
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
        #endregion
        #region Operations
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
        #endregion
        #region Finishing
        public IRule<T> Then<T>(Expression<Func<T, Action>> action)
        {
            return new Rule<T>((RuleConditionCollection<T>)_conditionCollection, new RuleAction<T>(action));
        }
        public IRule<T1, T2> Then<T1, T2>(Expression<Func<T1, Action>> action)
        {
            return new Rule<T1, T2>((RuleConditionCollection<T1, T2>)_conditionCollection, new RuleAction<T1>(action));
        }
        #endregion
        #region Helpers
        private void InitiateCondition<T, M>(Expression<Func<T, M>> member)
        {
            this._member = member;
            this._memberType = typeof(T);
        }
        private void AddCondition<T>(Operation operation, params T[] values)
        {
            var parameters = new object[] { this._member, operation, values };
            var ruleConditionType = typeof(RuleCondition<,>).MakeGenericType(_memberType, typeof(T));
            var condition = ruleConditionType.GetConstructors()[0].Invoke(parameters);
            var conditionCollectionType = _conditionCollection.GetType().GetMethod("Add", new [] { ruleConditionType}).Invoke(_conditionCollection, new object[] { condition });
        }
        #endregion
    }
}