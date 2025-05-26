using System.Text.Json.Serialization;

namespace ExamSystem.Models
{
    public class Questions
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string CorrectAnswer { get; set; } 

        public int ExamId { get; set; }
        [JsonIgnore]
        public Exam? Exam { get; set; }
    }
}
