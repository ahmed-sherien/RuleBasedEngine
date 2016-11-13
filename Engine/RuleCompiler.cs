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
        var operation = GetOperation(rule.Operation);
        var right = GetRightSide<T>(rule);
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
    ExpressionType GetOperation(Operation operation)
    {
        return operation.IsMethodCall ? ExpressionType.Call : (ExpressionType)Enum.Parse(typeof(ExpressionType), operation.Type.ToString());
    }
    ConstantExpression GetRightSide<T>(Rule rule)
    {
        var propertyType = typeof(T).GetProperty(rule.MemberName).PropertyType;
        if (rule.Operation.IsMethodCall)
        {
            var method = propertyType.GetMethod(rule.Operation.MethodName);
            if(method == null) throw new Exception($"There is no method named {rule.Operation} in the type {propertyType.Name}");
            var parameters = method.GetParameters();
            return Expression.Constant(Convert.ChangeType(rule.TargetValues[0], parameters[0].ParameterType));
        }
        return Expression.Constant(Convert.ChangeType(rule.TargetValues[0], propertyType));
    }
    Expression GetBuildedExperssion<T>(Rule rule, MemberExpression left, ExpressionType operation, ConstantExpression right)
    {
        if (rule.Operation.IsMethodCall)
        {
            var propertyType = typeof(T).GetProperty(rule.MemberName).PropertyType;
            var method = propertyType.GetMethod(rule.Operation.MethodName);
            return Expression.Call(left, method, right);
        }
        return Expression.MakeBinary(operation, left, right);
    }
}