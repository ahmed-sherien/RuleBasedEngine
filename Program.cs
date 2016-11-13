using System;
using System.Collections.Generic;

namespace RuleBasedEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rules = new List<Rule> {
                new Rule("Age", new Operation { IsMethodCall = false, Type = OperationType.GreaterThan }, "18"),
                new Rule("Person", new Operation { IsMethodCall = true, MethodName = "IsAdult" })
            };
            rules.ForEach(r => Console.WriteLine(r));

            var people = new List<Person> {
                new Person{ Age = 15 },
                new Person{ Age = 31 },
                new Person{ Age = 54 },
                new Person{ Age = 9 }
            };

            var compiler = new RuleCompiler();
            var matcher = new RuleMatcher(compiler);
            people.ForEach(person =>
            {
                Console.WriteLine(person);
                try
                {
                    rules.ForEach(r => Console.WriteLine(matcher.IsMatch<Person>(person, r)));
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            });
        }
        class Person
        {
            public int Age { get; set; }
            public bool IsAdult()
            {
                return Age >= 18;
            }

            public override string ToString()
            {
                return $"Person whose age is {Age}";
            }
        }
    }
}
