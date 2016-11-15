namespace RuleBasedEngine.Models
{
    public class Rule
    {
        public Condition Condition { get; set; }
        public string ActionName { get; set; }
        public Rule(Condition condition, string actionName)
        {
            Condition = condition;
            ActionName = actionName;
        }
        override public string ToString()
        {
            return $"If {Condition}, then {ActionName}";
        }
    }
}