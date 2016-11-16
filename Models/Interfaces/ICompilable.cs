using System.Linq.Expressions;

namespace RuleBasedEngine.Models.Interfaces
{
    public interface ICompilable<T>
    {
        T Compile();
        Expression<T> GenerateExpression();
    }
}