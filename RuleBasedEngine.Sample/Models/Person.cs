using System;
using System.Collections.Generic;
using System.Text;

namespace RuleBasedEngine.Sample.Models
{
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
}
