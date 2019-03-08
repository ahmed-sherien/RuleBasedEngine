namespace RuleBasedEngine.Models
{
    public class MatchResult<T1, T2> : MatchResult<T1>
    {
        public T2 ExtraItem { get; set; }
        public bool Item1IsMatch { get; set; }
        public bool Item2IsMatch { get; set; }

        public override string ToString()
        {
            return $"{typeof(T1).Name} is {(Item1IsMatch ? "" : "not ")}a match and {typeof(T2).Name} is {(Item2IsMatch ? "" : "not ")}a match";
        }
    }
}
