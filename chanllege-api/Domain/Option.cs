namespace Domain
{
    public class Option
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Question Question { get; set; }
    }
}