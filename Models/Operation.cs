public class Operation
{
    public bool IsMethodCall { get; set; }
    public OperationType Type { get; set; }
    public string MethodName { get; set; }
    public override string ToString()
    {
        return IsMethodCall ? MethodName : Type.ToString();
    }
}

public enum OperationType
{
    MethodCall,
    Equal,
    NotEqual,
    LessThan,
    LessThanOrEqual,
    GreaterThan,
    GreaterThanOrEqual
}