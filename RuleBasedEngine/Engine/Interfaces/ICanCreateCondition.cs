using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RuleBasedEngine.Engine.Interfaces
{
    public interface ICanCreateCondition
    {
        ICanAddIntOperation If<T>(Expression<Func<T, int>> member);
        ICanAddDoubleOperation If<T>(Expression<Func<T, double>> member);
        ICanAddFloatOperation If<T>(Expression<Func<T, float>> member);
        ICanAddDecimalOperation If<T>(Expression<Func<T, decimal>> member);
        ICanAddDateTimeOperation If<T>(Expression<Func<T, DateTime>> member);
        ICanAddStringOperation If<T>(Expression<Func<T, string>> member);
        ICanAddBoolOperation If<T>(Expression<Func<T, bool>> member);
        ICanAddOperation If<T, M>(Expression<Func<T, M>> member);
    }
}
