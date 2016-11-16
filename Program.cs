﻿using System.Collections.Generic;
using RuleBasedEngine.Models.Interfaces;
using RuleBasedEngine.Models;
using System;

namespace RuleBasedEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rules = new List<IRule<Person>> {
                new Rule<Person>(condition: new RuleCondition<Person, int>(p => p.Age, Operation.GreaterThan, 18), action: new RuleAction<Person>(p => p.GoToClub))
            };

            var people = new List<Person> {
                new Person{ Name = "Anas", Age = 15 },
                new Person{ Name = "Ahmed", Age = 31 },
                new Person{ Name = "Sameh", Age = 54 },
                new Person{ Name = "Janna", Age = 9 }
            };

            people.ForEach(person =>{
                Console.WriteLine("---------------------------------");
                Console.WriteLine(person);
                rules.ForEach(rule => {
                    Console.WriteLine("rule:---------------------------------");
                    Console.WriteLine(rule);
                    var match = rule.Match(person);
                    Console.WriteLine(match);
                    match.Execute();
                });
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
    class Club
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
