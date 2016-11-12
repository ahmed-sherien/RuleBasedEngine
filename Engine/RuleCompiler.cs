using System;
using System.Linq.Expressions;
using System.Reflection;

public class RuleCompiler
{
    public Func<T, bool> CompileRule<T>(Rule rule)
    {
        var parameter = Expression.Parameter(typeof(T));
        var expression = BuildExpression<T>(rule, parameter);
        return Expression.Lambda<Func<T, bool>>(expression, parameter).Compile();
    }
    Expression BuildExpression<T>(Rule rule, ParameterExpression parameter)
    {
        var left = GetLeftSide(parameter, rule.MemberName);
        var operation = GetOperation(rule.Operator);
        var right = GetRightSide<T>(rule, operation);
        return GetBuildedExperssion<T>(rule, left, operation, right);
    }
    MemberExpression GetLeftSide(ParameterExpression parameter, string memberName)
    {
        try
        {
            return MemberExpression.Property(parameter, memberName);
        }
        catch (ArgumentException ex)
        {
            throw new Exception($"No property in {parameter.Type.Name} named {memberName}", ex);
        }
    }
    ExpressionType GetOperation(string _operator)
    {
        ExpressionType expressionType;
        if (ExpressionType.TryParse(_operator, out expressionType)) return expressionType;
        return ExpressionType.Default;
    }
    ConstantExpression GetRightSide<T>(Rule rule, ExpressionType operation)
    {
        var propertyType = typeof(T).GetProperty(rule.MemberName).PropertyType;
        if (operation == ExpressionType.Default)
        {
            var method = propertyType.GetMethod(rule.Operator);
            if(method == null) throw new Exception($"There is no method named {rule.Operator} in the type {rule.MemberName}");
            var parameters = method.GetParameters();
            if (parameters.Length < 1 || parameters.Length > 1) throw new Exception($"There is no method named {rule.Operator} having a single parameter in the type {rule.MemberName}");
            return Expression.Constant(Convert.ChangeType(rule.TargetValue, parameters[0].ParameterType));
        }
        return Expression.Constant(Convert.ChangeType(rule.TargetValue, propertyType));
    }
    Expression GetBuildedExperssion<T>(Rule rule, MemberExpression left, ExpressionType operation, ConstantExpression right)
    {
        if (operation == ExpressionType.Default)
        {
            var propertyType = typeof(T).GetProperty(rule.MemberName).PropertyType;
            var method = propertyType.GetMethod(rule.Operator);
            return Expression.Call(left, method, right);
        }
        return Expression.MakeBinary(operation, left, right);
    }
}