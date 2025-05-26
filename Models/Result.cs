using Quiz.Models;

namespace ExamSystem.Models
{
    public class Result
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int Score { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
        public ICollection<Answer> Answers { get; set; }

    }
}
