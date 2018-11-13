using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Models
{
    partial class Operation
    {
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

        public static Operation IsTrue
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.IsTrue
                };
            }
        }

        public static Operation IsFalse
        {
            get
            {
                return new Operation
                {
                    IsMethodCall = false,
                    Type = OperationType.IsFalse
                };
            }
        }
    }
}
