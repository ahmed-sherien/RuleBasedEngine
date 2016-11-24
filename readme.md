# Rule Based Engine

based on a suggestion by [Martin Konicek](https://github.com/mkonicek) on his [Blog post](http://coding-time.blogspot.com.eg/2011/07/how-to-implement-rule-engine-in-c.html)

built using:

* .Net Core 1.0.1
* Visual Studio Code

to run:

1. clone
2. open command line in folder
3. type `dotnet run`

## Example:

```csharp
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public bool IsAdult { get { return Age > 18; } }
    public void GoToClub()
    {
        Console.WriteLine($"{Name} went to the club");
    }
}

class Club
{
    public string Name { get; set; }
    public bool IsOpen { get; set; }
}

var rule = RuleEngine.CreateRule<Person, Club>()
            .If<Person>(p => p.Age).GreaterThan(18)
            .AndIf<Club>(c => c.IsOpen).IsTrue()
            .Then<Person, Club>(p => p.GoToClub);

var people = new List<Person> {
    new Person{ Name = "Anas", Age = 15 },
    new Person{ Name = "Ahmed", Age = 31 },
    new Person{ Name = "Sameh", Age = 54 },
    new Person{ Name = "Janna", Age = 9 }
};
var club = new Club
{
    Name = "The Club",
    IsOpen = true
};

people.ForEach(person =>
{
    rule.Match(person, club).Execute();
});
```

output will be like:

```
rule:---------------------------------
If Person Age GreaterThan 18 and Club IsOpen IsTrue, then GoToClub
--------------------------------------
--[person 1]--------------------------
Person whose name is Anas, age is 15, and is not adult
Person is not a match
--------------------------------------
--[person 2]--------------------------
Person whose name is Ahmed, age is 31, and is adult
Person is a match
Ahmed went to the club
--------------------------------------
--[person 3]--------------------------
Person whose name is Sameh, age is 54, and is adult
Person is a match
Sameh went to the club
--------------------------------------
--[person 4]--------------------------
Person whose name is Janna, age is 9, and is not adult
Person is not a match
--------------------------------------
```