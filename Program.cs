using System.Collections.Generic;
using RuleBasedEngine.Models.Interfaces;
using RuleBasedEngine.Models;
using System;

namespace RuleBasedEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rules = new List<IRule<Person, Club>>
            {
                new Rule<Person, Club>(
                    mainConditions: new List<IRuleCondition<Person>>
                    {
                        new RuleCondition<Person, int>(p => p.Age, Operation.GreaterThan, 18)
                    },
                    extraConditions: new List<IRuleCondition<Club>>
                    {
                        new RuleCondition<Club, bool>(c => c.IsOpen, Operation.IsTrue)
                    },
                    action  : new RuleAction<Person>(p => p.GoToClub))
            };

            Console.WriteLine("--[rules]-------------------------------");
            rules.ForEach(rule => Console.WriteLine(rule));
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();

            var people = new List<Person>
            {
                new Person{ Name = "Anas", Age = 15 },
                new Person{ Name = "Ahmed", Age = 31 },
                new Person{ Name = "Sameh", Age = 54 },
                new Person{ Name = "Janna", Age = 9 }
            };

            var club = new Club
            {
                IsOpen = false
            };

            people.ForEach(person =>
            {
                Console.WriteLine($"--[person {people.IndexOf(person) + 1}]----------------------------");
                Console.WriteLine(person);
                rules.ForEach(rule =>
                {
                    Console.WriteLine($"--[rule {rules.IndexOf(rule) + 1}]------------------------------");
                    var match = rule.Match(person, club);
                    Console.WriteLine(match);
                    match.Execute();
                    Console.WriteLine("----------------------------------------");
                });
                Console.WriteLine("----------------------------------------");
                Console.WriteLine();
            });
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsAdult { get { return Age > 18; } }
        public void GoToClub()
        {
            Console.WriteLine($"{Name} went to the club");
        }

        public override string ToString()
        {
            return $"Person whose name is {Name}, age is {Age}, and is {(IsAdult ? string.Empty : "not ")}adult";
        }
    }
    public class Club
    {
        public bool IsOpen { get; set; }
        public void OpenClub()
        {
            IsOpen = true;
        }
        public void CloseClub()
        {
            IsOpen = false;
        }
    }
}
