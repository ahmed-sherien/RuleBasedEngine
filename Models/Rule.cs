using System.Collections.Generic;
using System.Linq;

public class Rule
{
    public string MemberName { get; set; }
    public Operation Operation { get; set; }
    public List<string> TargetValues { get; set; }
    public string ActionName { get; set; }
    public Rule(string memberName, Operation operation, string actionName, params string[] targetValues)
    {
        MemberName = memberName;
        Operation = operation;
        TargetValues = new List<string>();
        if (!targetValues.Any()) return;
        foreach (var value in targetValues)
        {
            TargetValues.Add(value);
        }
        ActionName = actionName;
    }
    override public string ToString()
    {
        return $"If {MemberName} {Operation}{(TargetValues.Any() ? " "+TargetValues.Aggregate((s1, s2) => $"{s1}, {s2}") : string.Empty)}, then {ActionName}";
    }
}