using RuleBasedEngine.Engine;
using RuleBasedEngine.Sample.Models;
using System;
using System.Collections.Generic;

namespace RuleBasedEngine.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Rule creation for Person class and Club class
            var rule = RuleEngine.CreateRule<Person, Club>()
                        // if person's age is greater than 18
                        .If<Person>(p => p.Age).GreaterThan(18)
                        // and if club is open
                        .AndIf<Club>(c => c.IsOpen).IsTrue()
                        // then person can go to club
                        .Then<Person, Club>(p => p.GoToClub);

            //printing out the rule
            Console.WriteLine("--[rules]-------------------------------");
            Console.WriteLine(rule);
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();

            //a list of people
            var people = new List<Person>
            {
                new Person{ Name = "Anas", Age = 15 },
                new Person{ Name = "Ahmed", Age = 31 },
                new Person{ Name = "Sameh", Age = 54 },
                new Person{ Name = "Janna", Age = 9 }
            };

            //the club
            var club = new Club
            {
                Name = "The Club",
                IsOpen = true
            };

            //checking the rulw for every person in the list and the club
            people.ForEach(person =>
            {
                //printing out the person and the club
                Console.WriteLine($"--[person {people.IndexOf(person) + 1}]----------------------------");
                Console.WriteLine(person);
                Console.WriteLine(club);

                // checking if person and club match the rule
                var match = rule.Match(person, club);

                // printing out the match result
                Console.WriteLine(match);

                // executing the action if the person and club match the rule
                match.Execute();

                Console.WriteLine("----------------------------------------");
                Console.WriteLine();
            });
        }
    }
}
