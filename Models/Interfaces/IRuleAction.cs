using System;

namespace RuleBasedEngine.Models.Interfaces
{
    public interface IRuleAction<T>
    {
        Func<T, Action> Method {get; set;}
    }
}