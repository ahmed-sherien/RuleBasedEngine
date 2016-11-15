namespace RuleBasedEngine.Models
{
    public class Operation
    {
        private Operation() { }
        public Operation(string methodName)
        {
            IsMethodCall = true;
            MethodName = methodName;
        }
        public bool IsMethodCall { get; set; }
        public OperationType Type { get; set; }
        public string MethodName { get; set; }

        public override string ToString()
        {
            return IsMethodCall ? MethodName : Type.ToString();
        }

        public static Operation Equal
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.Equal
                };
            }
        }
        public static Operation NotEqual
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.NotEqual
                };
            }
        }
        public static Operation LessThan
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.LessThan
                };
            }
        }
        public static Operation LessThanOrEqual
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.LessThanOrEqual
                };
            }
        }
        public static Operation GreaterThan
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.GreaterThan
                };
            }
        }
        public static Operation GreaterThanOrEqual
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.GreaterThanOrEqual
                };
            }
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
}