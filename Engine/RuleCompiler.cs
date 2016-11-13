using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

public class RuleCompiler
{
    public Func<T, bool> CompileRule<T>(Rule rule)
    {
        var parameter = Expression.Parameter(typeof(T));
        var expression = BuildExpression(rule, parameter);
        return Expression.Lambda<Func<T, bool>>(expression, parameter).Compile();
    }
    Expression BuildExpression(Rule rule, ParameterExpression parameter)
    {
        var member = GetMember(parameter, rule.MemberName);
        var expressionType = GetExpressionType(rule.Operation);
        var constants = GetConstants(member, rule);
        return GetBuildedExperssion(rule, member, expressionType, constants);
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
    List<ConstantExpression> GetConstants(Expression member, Rule rule)
    {
        var type = member.Type;
        if (rule.Operation.IsMethodCall)
        {
            var method = type.GetMethod(rule.Operation.MethodName);
            if (method == null) throw new Exception($"There is no method named {rule.Operation} in the type {type.Name}");
            var parameters = method.GetParameters().ToList();
            if (parameters.Count != rule.TargetValues.Count) throw new Exception($"There is no method named {rule.Operation} in the type {type.Name} taking the same count of input");
            return parameters.Select(p => Expression.Constant(Convert.ChangeType(rule.TargetValues[parameters.IndexOf(p)], p.ParameterType))).ToList();
        }
        return new List<ConstantExpression> { Expression.Constant(Convert.ChangeType(rule.TargetValues[0], type)) };
    }
    Expression GetBuildedExperssion(Rule rule, Expression member, ExpressionType expressionType, List<ConstantExpression> constants)
    {
        if (rule.Operation.IsMethodCall)
        {
            var type = member.Type;
            var method = type.GetMethod(rule.Operation.MethodName);
            return Expression.Call(member, method, constants);
        }
        return Expression.MakeBinary(expressionType, member, constants[0]);
    }
}