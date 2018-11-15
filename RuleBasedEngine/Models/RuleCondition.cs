using RuleBasedEngine.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RuleBasedEngine.Models
{
    public class RuleCondition<T, M> : IRuleCondition<T>
    {
        /// <summary>
        /// RuleCondition constructor
        /// </summary>
        /// <param name="member">Expression to instance member</param>
        /// <param name="operation">Operation to be applied on member</param>
        /// <param name="targetValues">Values related to the operation in hand</param>
        public RuleCondition(Expression<Func<T, M>> member, Operation operation, params M[] targetValues)
        {
            Member = member;
            Operation = operation;
            TargetValues = targetValues.ToList();
        }

        public Expression<Func<T, M>> Member { get; set; }
        public Operation Operation { get; set; }
        public List<M> TargetValues { get; set; }

        public bool IsMatch(T member)
        {
            return GenerateExpression().Compile().Invoke(member);
        }

        public Expression<Func<T, bool>> GenerateExpression()
        {
            // generate a parameter expression
            var parameter = Expression.Parameter(typeof(T));
            // get member name
            var memberName = ((MemberExpression)Member.Body).Member.Name;
            // generate a member expression
            var member = parameter.Type.Name == memberName ? (Expression)parameter : MemberExpression.Property(parameter, memberName);
            // generate lambda expression body
            Expression expression = Operation.GetExpression<M>(member, TargetValues);
            // return lambda expression
            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }

        public override string ToString()
        {
            return $"{typeof(T).Name} {((MemberExpression)Member.Body).Member.Name} {Operation} {(TargetValues.Any() ? TargetValues.Select(v => v.ToString()).Aggregate((s1, s2) => $"{s1}, {s2}") : string.Empty)}".Trim();
        }
    }
}
