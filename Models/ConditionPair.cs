using System;
using System.Linq.Expressions;
using RuleBasedEngine.Models.Interfaces;

namespace RuleBasedEngine.Models
{
    public class ConditionPair<T1, T2> : ICondition<T1, T2>
    {
        public Condition<T1> FirstCondition { get; set; }
        public Condition<T2> SecondCondition { get; set; }
        public OperationType OperationType { get; set; }

        public Func<T1, T2, bool> Compile()
        {
            return GenerateExpression().Compile();
        }

        public Expression<Func<T1, T2, bool>> GenerateExpression()
        {
            var parameter1 = Expression.Parameter(typeof(T1));
            var parameter2 = Expression.Parameter(typeof(T2));
            var expression1 = FirstCondition.GenerateExpression();
            var expression2 = SecondCondition.GenerateExpression();
            var expressionType = (ExpressionType)Enum.Parse(typeof(ExpressionType), OperationType.ToString());
            var binaryExpression = Expression.MakeBinary(expressionType, expression1, expression2);
            return Expression.Lambda<Func<T1, T2, bool>>(binaryExpression, parameter1, parameter2);
        }
    }
}