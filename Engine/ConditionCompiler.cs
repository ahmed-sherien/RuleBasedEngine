using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using RuleBasedEngine.Models;

namespace RuleBasedEngine.Engine
{
    public class ConditionCompiler
    {
        public Func<T, bool> CompileRule<T>(Condition condition)
        {
            var parameter = Expression.Parameter(typeof(T));
            var expression = BuildExpression(condition, parameter);
            return Expression.Lambda<Func<T, bool>>(expression, parameter).Compile();
        }
        Expression BuildExpression(Condition condition, ParameterExpression parameter)
        {
            var member = GetMember(parameter, condition.MemberName);
            var expressionType = GetExpressionType(condition.Operation);
            var constants = GetConstants(member, condition);
            return GetBuildedExperssion(condition, member, expressionType, constants);
        }
        Expression GetMember(ParameterExpression parameter, string memberName)
        {
            try
            {
                if (parameter.Type.Name == memberName) return parameter;
                return MemberExpression.Property(parameter, memberName);
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"No property in {parameter.Type.Name} named {memberName}", ex);
            }
        }
        ExpressionType GetExpressionType(Operation operation)
        {
            return operation.IsMethodCall ? ExpressionType.Call : (ExpressionType)Enum.Parse(typeof(ExpressionType), operation.Type.ToString());
        }
        List<ConstantExpression> GetConstants(Expression member, Condition condition)
        {
            var type = member.Type;
            if (condition.Operation.IsMethodCall)
            {
                var method = type.GetMethod(condition.Operation.MethodName);
                if (method == null) throw new Exception($"There is no method named {condition.Operation} in the type {type.Name}");
                var parameters = method.GetParameters().ToList();
                if (parameters.Count != condition.TargetValues.Count) throw new Exception($"There is no method named {condition.Operation} in the type {type.Name} taking the same count of input");
                return parameters.Select(p => Expression.Constant(Convert.ChangeType(condition.TargetValues[parameters.IndexOf(p)], p.ParameterType))).ToList();
            }
            return new List<ConstantExpression> { Expression.Constant(Convert.ChangeType(condition.TargetValues[0], type)) };
        }
        Expression GetBuildedExperssion(Condition condition, Expression member, ExpressionType expressionType, List<ConstantExpression> constants)
        {
            if (condition.Operation.IsMethodCall)
            {
                var type = member.Type;
                var method = type.GetMethod(condition.Operation.MethodName);
                return Expression.Call(member, method, constants);
            }
            return Expression.MakeBinary(expressionType, member, constants[0]);
        }
    }
}