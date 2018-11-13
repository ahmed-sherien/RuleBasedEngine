using RuleBasedEngine.Engine.Interfaces;
using RuleBasedEngine.Models;
using RuleBasedEngine.Models.Interfaces;
using System;
using System.Linq.Expressions;

namespace RuleBasedEngine.Engine
{
    public partial class RuleEngine
    {
        /// <summary>
        /// Create a Rule applied on a single type
        /// </summary>
        /// <typeparam name="T">Type of class to apply rule and to execute action</typeparam>
        /// <returns>An instance of RuleEngine to start rule creation</returns>
        public static ICanCreateCondition CreateRule<T>()
        {
            return new RuleEngine(new RuleConditionCollection<T>(), typeof(T));
        }

        /// <summary>
        /// Create a Rule applied on 2 types
        /// </summary>
        /// <typeparam name="T1">First type of class to apply rule and to execute action</typeparam>
        /// <typeparam name="T2">Second type of class to apply rule and to execute action</typeparam>
        /// <returns>An instance of RuleEngine to start rule creation</returns>
        public static ICanCreateCondition CreateRule<T1, T2>()
        {
            return new RuleEngine(new RuleConditionCollection<T1, T2>(), typeof(T1), typeof(T2));
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="conditionCollection">Collection of condition to be applied on the instance of selected types</param>
        /// <param name="types">Selected types of apply rule and to execute action</param>
        private RuleEngine(IRuleConditionCollection conditionCollection, params Type[] types)
        {
            _conditionCollection = conditionCollection;
            _types = types;
        }

        private Type[] _types;
        private IRuleConditionCollection _conditionCollection;
        private Expression _memberExpression;
        private Type _instanceType;

        /// <summary>
        /// Initiating a new condition to be set
        /// </summary>
        /// <typeparam name="T">Type of instance for condition to be applied</typeparam>
        /// <typeparam name="M">Type of member of instance for condition to be applied</typeparam>
        /// <param name="expression">Member expression of instance for condition to be applied</param>
        private void InitiateCondition<T, M>(Expression<Func<T, M>> expression)
        {
            _memberExpression = expression;
            _instanceType = typeof(T);
        }

        /// <summary>
        /// Adding the created condition to RuleConditionCollection
        /// </summary>
        /// <typeparam name="T">Type of member of instance for condition to be applied</typeparam>
        /// <param name="operation">Operation to be run on instance member</param>
        /// <param name="values">Values related to the opration in hand</param>
        private void AddCondition<T>(Operation operation, params T[] values)
        {
            // creating an array of RuleCondition constructor paramters
            var parameters = new object[] { _memberExpression, operation, values };
            
            // generating a RuleCondition generic type using the instance type and the member type
            var ruleConditionType = typeof(RuleCondition<,>).MakeGenericType(_instanceType, typeof(T));
            
            // constructing the RuleCondition instance
            var condition = ruleConditionType.GetConstructors()[0].Invoke(parameters);

            // adding the RuleCondition object to the RuleConditionCollection instance
            _conditionCollection.GetType().GetMethod("Add", new[] { ruleConditionType }).Invoke(_conditionCollection, new object[] { condition });
        }
    }
}