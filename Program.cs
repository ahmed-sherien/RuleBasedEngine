using System;
using System.Collections.Generic;
using RuleBasedEngine.Engine;
using RuleBasedEngine.Models;

namespace RuleBasedEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rules = new List<Rule<Person>> {
                //new Rule<Person>(Condition<Person>.GreaterThanCondition("Age", "18"), "BecomeAdult"),
                new Rule<Person>(Condition<Person>.GenerateCondition<int>(p => p.Age, Operation.GreaterThan, 18), "BecomeAdult")
            };
            rules.ForEach(rule => Console.WriteLine(rule));

            var people = new List<Person> {
                new Person{ Age = 15 },
                new Person{ Age = 31 },
                new Person{ Age = 54 },
                new Person{ Age = 9 }
            };

            var matcher = new RuleMatcher<Person>();
            var executer = new RuleExecuter();
            people.ForEach(person =>
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine(person);
                try
                {
                    rules.ForEach(rule =>
                    {
                        var match = matcher.IsMatch(person, rule);
                        Console.WriteLine(match);
                        executer.ExecuteAction<Person>(match, rule.ActionName);
                        Console.WriteLine(person);
                    });
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                finally
                {
                    Console.WriteLine("-----------------------------");
                }
            });
        }
        class Person
        {
            public int Age { get; set; }
            public bool IsAdult { get; set; }
            public void BecomeAdult()
            {
                IsAdult = true;
            }

            public override string ToString()
            {
                return $"Person whose age is {Age}, and is {(IsAdult ? string.Empty : "not ")}adult";
            }
        }
    }
}
