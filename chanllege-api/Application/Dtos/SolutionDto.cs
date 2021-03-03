using System;
using System.Collections.Generic;
using Domain;

namespace Application.Dtos
{
    public class SolutionDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int QuizId { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}