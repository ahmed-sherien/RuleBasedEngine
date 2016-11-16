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

```c#
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

var rule = new Rule<Person>(
    condition: new RuleCondition<Person, int>(p => p.Age, Operation.GreaterThan, 18),
    action: new RuleAction<Person>(p => p.GoToClub)
    );

var people = new List<Person> {
    new Person{ Name = "Anas", Age = 15 },
    new Person{ Name = "Ahmed", Age = 31 },
    new Person{ Name = "Sameh", Age = 54 },
    new Person{ Name = "Janna", Age = 9 }
};

people.ForEach(person =>
{
    rule.Match(person).Execute();
});
```