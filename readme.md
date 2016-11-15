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
var rule = new Rule(Condition.GreaterThanCondition("Age", "18"), "BecomeAdult");

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
    var match = matcher.IsMatch<Person>(person, rule);
    executer.ExecuteAction<Person>(match, rule.ActionName);
});
```