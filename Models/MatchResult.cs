namespace RuleBasedEngine.Models
{
    public class MatchResult<T>
    {
        public bool IsMatch { get; set; }
        public Rule Rule { get; set; }
        public T Item { get; set; }
        public override string ToString()
        {
            return $"{typeof(T).Name} does {(IsMatch ? "" : "not ")}match the rule: \"{Rule}\"";
        }
    }
}