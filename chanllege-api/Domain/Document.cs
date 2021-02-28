namespace Domain
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Question Question { get; set; }
        public AppUser User { get; set; }
    }
}