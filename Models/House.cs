namespace GoT_Wiki.Models
{
    /// <summary>
    /// Class <c>House</c> models a GoT house.
    /// </summary>
    public class House
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string CoatOfArms { get; set; }
        public string Words { get; set; }
        public string[] Titles { get; set; }
        public string[] Seats { get; set; }
        public string CurrentLord { get; set; }
        public string Heir { get; set; }
        public string Overlord { get; set; }
        public string Founded { get; set; }
        public string Founder { get; set; }
        public string DiedOut { get; set; }
        public string[] AncestralWeapons { get; set; }
        public string[] CadetBranches { get; set; }
        public string[] SwornMembers { get; set; }
    }

}
