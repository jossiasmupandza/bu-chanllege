using System;
using System.Collections.Generic;
using Domain;

namespace Application.Dtos
{
    public class QuizDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool PublicQuiz { get; set; }
        public bool PublicAnswer { get; set; }
        public string QuizToken { get; set; }
        public string EditToken { get; set; }
        public string NotificationEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Category { get; set; }
        public string CreatorFullName { get; set; }
        public int CreatorId { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
        public List<Solution> Solutions { get; set; } = new List<Solution>();
    }
}