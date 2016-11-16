using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using RuleBasedEngine.Models.Interfaces;

namespace RuleBasedEngine.Models
{
    public class RuleCondition<T, M> : IRuleCondition<T>
    {
        public RuleCondition(Expression<Func<T, M>> member, Operation operation, params M[] targetValues)
        {
            Member = member;
            Operation = operation;
            TargetValues = targetValues.ToList();
        }
        public Expression<Func<T, M>> Member { get; set; }
        public Operation Operation { get; set; }
        public List<M> TargetValues { get; set; }

        public Func<T, bool> Compile()
        {
            return GenerateExpression().Compile();
        }

        public Expression<Func<T, bool>> GenerateExpression()
        {
            var parameter = Expression.Parameter(typeof(T));
            var memberName = ((MemberExpression)Member.Body).Member.Name;
            var member = parameter.Type.Name == memberName ? (Expression)parameter : MemberExpression.Property(parameter, memberName);
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
        public override string ToString()
        {
            return $"{((MemberExpression)Member.Body).Member.Name} {Operation}{(TargetValues.Any() ? " " + TargetValues.Select(v => v.ToString()).Aggregate((s1, s2) => $"{s1}, {s2}") : string.Empty)}";
        }
    }
}