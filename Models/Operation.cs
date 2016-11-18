using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RuleBasedEngine.Models
{
    public class Operation
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

        public Expression GetExpression<T>(Expression member, List<T> targetValues)
        {
            var type = member.Type;
            if (IsMethodCall)
            {
                var method = type.GetMethod(MethodName);
                if (method == null) throw new Exception($"There is no method named {this} in the type {type.Name}");
                var parameters = method.GetParameters().ToList();
                if (parameters.Count != targetValues.Count) throw new Exception($"There is no method named {this} in the type {type.Name} taking the same count of input");
                var constants = parameters.Select(p => Expression.Constant(Convert.ChangeType(targetValues[parameters.IndexOf(p)], p.ParameterType))).ToList();
                return Expression.Call(member, method, constants);
            }
            var expressionType = (ExpressionType)Enum.Parse(typeof(ExpressionType), Type.ToString());
            if (BinaryOperations.Contains(Type))
            {
                var constant = Expression.Constant(Convert.ChangeType(targetValues[0], type));
                return Expression.MakeBinary(expressionType, member, constant);
            }
            else if (UnaryOperations.Contains(Type))
                return Expression.MakeUnary(expressionType, member, typeof(bool));
            throw new Exception("Unknown Operation");
        }

        public override string ToString()
        {
            return IsMethodCall ? MethodName : Type.ToString();
        }

        public static Operation Equal
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.Equal
                };
            }
        }
        public static Operation NotEqual
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.NotEqual
                };
            }
        }
        public static Operation LessThan
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.LessThan
                };
            }
        }
        public static Operation LessThanOrEqual
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.LessThanOrEqual
                };
            }
        }
        public static Operation GreaterThan
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.GreaterThan
                };
            }
        }
        public static Operation GreaterThanOrEqual
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.GreaterThanOrEqual
                };
            }
        }
        public static Operation IsTrue
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.IsTrue
                };
            }
        }
        public static Operation IsFalse
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.IsFalse
                };
            }
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

    public enum OperationType
    {
        MethodCall,
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        IsTrue,
        IsFalse
    }
}