using System;

namespace GoT_Wiki.Models
{
    public class Book
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public string[] Authors { get; set; }
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }
        public string Country { get; set; }
        public string MediaType { get; set; }
        public DateTime Released { get; set; }
        public string[] Characters { get; set; }
        public string[] PovCharacters { get; set; }
    }
}
