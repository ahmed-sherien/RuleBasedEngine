using System;
using System.Collections.Generic;

namespace RuleBasedEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rule = new Rule("Age", new Operation { IsMethodCall = false, Type = OperationType.GreaterThan }, "18");
            Console.WriteLine(rule);

            var users = new List<User> {
                new User{ Age = 15 },
                new User{ Age = 31 },
                new User{ Age = 54 },
                new User{ Age = 9 }
            };

            var compiler = new RuleCompiler();
            var matcher = new RuleMatcher(compiler);
            foreach (var user in users)
            {
                Console.WriteLine(user);
                try
                {
                    Console.WriteLine(matcher.IsMatch<User>(user, rule));
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        class User
        {
            public int Age { get; set; }
            
            public override string ToString()
            {
                return $"User whose age is {Age}";
            }
        }
    }
}
