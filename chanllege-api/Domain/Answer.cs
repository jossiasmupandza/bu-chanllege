using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class Answer
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Solution Solution { get; set; }
        public Question Question { get; set; }
        public virtual ICollection<Option> Options { get; set; } = new List<Option>();
        public ICollection<AnswerOption> AnswerOptions { get; set; }
    }
}