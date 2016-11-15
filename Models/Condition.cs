using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using RuleBasedEngine.Models.Interfaces;

namespace RuleBasedEngine.Models
{
    public class Condition<T> : ICondition<T>
    {
        public Condition(Type T, string memberName, Operation operation, params string[] targetValues)
        {
            MemberName = memberName;
            Operation = operation;
            TargetValues = new List<string>();
            if (!targetValues.Any()) return;
            foreach (var value in targetValues)
            {
                TargetValues.Add(value);
            }
        }
        public string MemberName { get; set; }
        public Operation Operation { get; set; }
        public List<string> TargetValues { get; set; }
        public static Condition<T> EqualCondition(string memberName, params string[] targetValues)
        {
            return new Condition<T>(typeof(T), memberName, Operation.Equal, targetValues);
        }
        public static Condition<T> NotEqualCondition(string memberName, params string[] targetValues)
        {
            return new Condition<T>(typeof(T), memberName, Operation.NotEqual, targetValues);
        }
        public static Condition<T> GreaterThanCondition(string memberName, params string[] targetValues)
        {
            return new Condition<T>(typeof(T), memberName, Operation.GreaterThan, targetValues);
        }
        public static Condition<T> GreaterThanOrEqualCondition(string memberName, params string[] targetValues)
        {
            return new Condition<T>(typeof(T), memberName, Operation.GreaterThanOrEqual, targetValues);
        }
        public static Condition<T> LessThanCondition(string memberName, params string[] targetValues)
        {
            return new Condition<T>(typeof(T), memberName, Operation.LessThan, targetValues);
        }
        public static Condition<T> LessThanOrEqualCondition(string memberName, params string[] targetValues)
        {
            return new Condition<T>(typeof(T), memberName, Operation.LessThanOrEqual, targetValues);
        }

        public static Condition<T> GenerateCondition<M>(Expression<Func<T, M>> member, Operation operation, params M[] targetValues)
        {
            return new Condition<T>(typeof(T), ((MemberExpression)member.Body).Member.Name, operation, targetValues.Select(v => v.ToString()).ToArray());
        }
        public override string ToString()
        {
            return $"{MemberName} {Operation}{(TargetValues.Any() ? " " + TargetValues.Aggregate((s1, s2) => $"{s1}, {s2}") : string.Empty)}";
        }

        public Func<T, bool> Compile()
        {
            return GenerateExpression().Compile();
        }

        public Expression<Func<T, bool>> GenerateExpression()
        {
            var parameter = Expression.Parameter(typeof(T));
            var member = parameter.Type.Name == MemberName ? (Expression)parameter : MemberExpression.Property(parameter, MemberName);
            var expressionType = Operation.IsMethodCall ? ExpressionType.Call : (ExpressionType)Enum.Parse(typeof(ExpressionType), Operation.Type.ToString());
            var type = member.Type;
            Expression expression;
            if (Operation.IsMethodCall)
            {
                var method = type.GetMethod(Operation.MethodName);
                if (method == null) throw new Exception($"There is no method named {Operation} in the type {type.Name}");
                var parameters = method.GetParameters().ToList();
                if (parameters.Count != TargetValues.Count) throw new Exception($"There is no method named {Operation} in the type {type.Name} taking the same count of input");
                var constants = parameters.Select(p => Expression.Constant(Convert.ChangeType(TargetValues[parameters.IndexOf(p)], p.ParameterType))).ToList();
                expression = Expression.Call(member, method, constants);
            }
            else
            {
                var constant = Expression.Constant(Convert.ChangeType(TargetValues[0], type));
                expression = Expression.MakeBinary(expressionType, member, constant);
            }
            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }
    }
}