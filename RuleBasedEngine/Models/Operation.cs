using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RuleBasedEngine.Models
{
    public partial class Operation
    {
        private Operation() { }

        public Operation(string methodName)
        {
            IsMethodCall = true;
            MethodName = methodName;
        }

        public bool IsMethodCall { get; set; }
        public OperationType Type { get; set; }
        public string MethodName { get; set; }

        /// <summary>
        /// Generate the operation expression
        /// </summary>
        /// <typeparam name="T">Type of the values to apply operation on</typeparam>
        /// <param name="member">Member to apply operation on</param>
        /// <param name="targetValues">Values to apply operation on</param>
        /// <returns>Operation expression</returns>
        public Expression GetExpression<T>(Expression member, List<T> targetValues)
        {
            var type = member.Type;

            // check if the member is a method call
            if (IsMethodCall)
            {
                // get method by name
                var method = type.GetMethod(MethodName);
                if (method == null)
                {
                    throw new Exception($"There is no method named {this} in the type {type.Name}");
                }

                // get parameters list
                var parameters = method.GetParameters().ToList();
                // check if values match parameters count
                if (parameters.Count != targetValues.Count)
                {
                    throw new Exception($"There is no method named {this} in the type {type.Name} taking the same count of input");
                }

                // convert values to a list of ConstantExpressions
                var constants = parameters.Select(p => Expression.Constant(Convert.ChangeType(targetValues[parameters.IndexOf(p)], p.ParameterType))).ToList();
                // generate a MethodCallExpression
                return Expression.Call(member, method, constants);
            }

            // if not a method call
            // then validate that the member type is one of the predefined operations
            if (Enum.TryParse(Type.ToString(), out ExpressionType expressionType))
            {
                // check if it is in the binary operations
                if (BinaryOperations.Contains(Type))
                {
                    // convert value to ConstantExpression
                    var constant = Expression.Constant(Convert.ChangeType(targetValues[0], type));
                    // generate a BinaryExpression
                    return Expression.MakeBinary(expressionType, member, constant);
                }
                else if (UnaryOperations.Contains(Type))
                {
                    // generate a UnaryExpression
                    return Expression.MakeUnary(expressionType, member, typeof(bool));
                }
            }
            // throw an exception if operation not found
            throw new Exception("Unknown Operation");
        }

        public override string ToString()
        {
            return IsMethodCall ? MethodName : Type.ToString();
        }

        private List<OperationType> BinaryOperations
        {
            get
            {
                return new List<OperationType>{
                    OperationType.Equal,
                    OperationType.NotEqual,
                    OperationType.GreaterThan,
                    OperationType.GreaterThanOrEqual,
                    OperationType.LessThan,
                    OperationType.LessThanOrEqual,
                };
            }
        }

        private List<OperationType> UnaryOperations
        {
            get
            {
                return new List<OperationType>{
                    OperationType.IsTrue,
                    OperationType.IsFalse,
                };
            }
        }
    }
}
