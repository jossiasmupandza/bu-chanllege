using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Public { get; set; }
        public string QuizToken { get; set; }
        [Required] public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Category Category { get; set; }
        public AppUser User { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<Solution> Solutions { get; set; } = new List<Solution>();
    }
}