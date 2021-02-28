using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Domain
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Required { get; set; }
        public bool MultipleOptions { get; set; }
        public Quiz Quiz { get; set; }
        public InputType InputType { get; set; }
        public ICollection<Document> Documents { get; set; } = new List<Document>();
        public ICollection<Option> Options { get; set; } = new List<Option>();
    }
}