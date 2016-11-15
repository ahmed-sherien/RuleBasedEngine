using System.Collections.Generic;
using System.Linq;

namespace RuleBasedEngine.Models
{
    public class Condition
    {
        public Condition(string memberName, Operation operation, params string[] targetValues)
        {
            MemberName = memberName;
            Operation = operation;
            TargetValues = new List<string>();
            if (!targetValues.Any()) return;
            foreach (var value in targetValues)
            {
                TargetValues.Add(value);
            }
        }
        public string MemberName { get; set; }
        public Operation Operation { get; set; }
        public List<string> TargetValues { get; set; }
        public static Condition EqualCondition(string memberName, params string[] targetValues)
        {
            return new Condition(memberName, Operation.Equal, targetValues);
        }
        public static Condition NotEqualCondition(string memberName, params string[] targetValues)
        {
            return new Condition(memberName, Operation.NotEqual, targetValues);
        }
        public static Condition GreaterThanCondition(string memberName, params string[] targetValues)
        {
            return new Condition(memberName, Operation.GreaterThan, targetValues);
        }
        public static Condition GreaterThanOrEqualCondition(string memberName, params string[] targetValues)
        {
            return new Condition(memberName, Operation.GreaterThanOrEqual, targetValues);
        }
        public static Condition LessThanCondition(string memberName, params string[] targetValues)
        {
            return new Condition(memberName, Operation.LessThan, targetValues);
        }
        public static Condition LessThanOrEqualCondition(string memberName, params string[] targetValues)
        {
            return new Condition(memberName, Operation.LessThanOrEqual, targetValues);
        }
        public override string ToString()
        {
            return $"{MemberName} {Operation}{(TargetValues.Any() ? " " + TargetValues.Aggregate((s1, s2) => $"{s1}, {s2}") : string.Empty)}";
        }
    }
}