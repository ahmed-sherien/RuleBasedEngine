﻿using RuleBasedEngine.Models.Interfaces;
using System;
using System.Linq.Expressions;

namespace RuleBasedEngine.Engine.Interfaces
{
    public interface ICanAddConditionOrAction
    {
        ICanAddIntOperation AndIf<T>(Expression<Func<T, int>> member);
        ICanAddDateTimeOperation AndIf<T>(Expression<Func<T, DateTime>> member);
        ICanAddStringOperation AndIf<T>(Expression<Func<T, string>> member);
        ICanAddBoolOperation AndIf<T>(Expression<Func<T, bool>> member);
        ICanAddOperation AndIf<T, M>(Expression<Func<T, M>> member);
        IRule<T> Then<T>(Expression<Func<T, Action>> action);
        IRule<T1, T2> Then<T1, T2>(Expression<Func<T1, Action>> action);
        IRule<T> Validate<T>();
        IRule<T1, T2> Validate<T1, T2>();
    }
}
