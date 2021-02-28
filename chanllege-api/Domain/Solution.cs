using System;
using System.Collections.Generic;

namespace Domain
{
    public class Solution
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}