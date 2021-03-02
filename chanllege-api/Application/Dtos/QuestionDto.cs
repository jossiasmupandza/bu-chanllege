using System.Collections.Generic;
using Domain;

namespace Application.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Required { get; set; }
        public bool MultipleOptions { get; set; }
        public int QuizId { get; set; } 
        public InputType InputType { get; set; }
        public Document Document { get; set; }
        public List<OptionDto> Options { get; set; } = new List<OptionDto>();
    }
}