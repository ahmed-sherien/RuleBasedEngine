using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Models
{
    public enum OperationType
    {
        MethodCall,
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        IsTrue,
        IsFalse
    }
}
