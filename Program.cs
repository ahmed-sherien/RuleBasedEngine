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
            var rules = new List<Rule> {
                new Rule(Condition.GreaterThanCondition("Age", "18"), "BecomeAdult"),
            };
            rules.ForEach(r => Console.WriteLine(r));

            var people = new List<Person> {
                new Person{ Age = 15 },
                new Person{ Age = 31 },
                new Person{ Age = 54 },
                new Person{ Age = 9 }
            };

            var matcher = new RuleMatcher();
            var executer = new RuleExecuter();
            people.ForEach(person =>
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine(person);
                try
                {
                    rules.ForEach(r =>
                    {
                        var match = matcher.IsMatch<Person>(person, r);
                        Console.WriteLine(match);
                        executer.ExecuteAction<Person>(match, r.ActionName);
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
