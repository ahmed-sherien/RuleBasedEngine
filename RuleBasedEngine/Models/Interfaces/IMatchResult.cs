namespace RuleBasedEngine.Models.Interfaces
{
    public interface IMatchResult
    {
        bool IsMatch { get; set; }
        void Execute();
    }
}
