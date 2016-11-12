public class Rule
{
    public string MemberName { get; set; }
    public string Operator { get; set; }
    public string TargetValue { get; set; }
    public Rule (string memberName, string _operator, string targetValue)
    {
        MemberName = memberName;
        Operator = _operator;
        TargetValue = targetValue;
    }
    override public string ToString()
    {
        return $"{MemberName} should be {Operator} {TargetValue}";
    }
}