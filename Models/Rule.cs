using System.Collections.Generic;
using System.Linq;

public class Rule
{
    public string MemberName { get; set; }
    public Operation Operation { get; set; }
    public List<string> TargetValues { get; set; }
    public Rule(string memberName, Operation operation, params string[] targetValues)
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
    override public string ToString()
    {
        return $"{MemberName} {Operation}{(TargetValues.Any() ? " "+TargetValues.Aggregate((s1, s2) => $"{s1}, {s2}") : string.Empty)}";
    }
}