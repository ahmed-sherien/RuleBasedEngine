namespace RuleBasedEngine.Sample.Models
{
    public class Club
    {
        public string Name { get; set; }
        public bool IsOpen { get; set; }

        public void OpenClub()
        {
            IsOpen = true;
        }

        public void CloseClub()
        {
            IsOpen = false;
        }

        public override string ToString()
        {
            return $"Club \"{Name}\" is {(IsOpen ? string.Empty : "not ")}open";
        }
    }
}
