namespace Domain
{
    public class AnswerOption
    {
        public int Id { get; set; }
        public Answer Answer { get; set; }
        public Option Option { get; set; }
    }
}