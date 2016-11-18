using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            Expression expression = Operation.GetExpression<M>(member, TargetValues);
            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }
        public override string ToString()
        {
            return $"{typeof(T).Name} {((MemberExpression)Member.Body).Member.Name} {Operation} {(TargetValues.Any() ? TargetValues.Select(v => v.ToString()).Aggregate((s1, s2) => $"{s1}, {s2}") : string.Empty)}".Trim();
        }
    }
}