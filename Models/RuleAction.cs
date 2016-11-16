using System;
using System.Reflection;
using System.Linq.Expressions;
using RuleBasedEngine.Models.Interfaces;

namespace RuleBasedEngine.Models
{
    public class RuleAction<T> : IRuleAction<T>
    {
        private Expression<Func<T, Action>> _action;
        public Func<T, Action> Method {get; set;}
        public RuleAction(Expression<Func<T, Action>> action)
        {
            _action = action;
            Method = action.Compile();
        }
        public override string ToString()
        {
            return $"{((MethodInfo)((ConstantExpression)((MethodCallExpression)((UnaryExpression)_action.Body).Operand).Object).Value).Name}";
        }
    }
}