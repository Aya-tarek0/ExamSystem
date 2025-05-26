using Quiz.Models;

namespace ExamSystem.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }


        public string? UserId { set; get; }

        public ApplicationUser? user{ set; get; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Questions>? questions { get; set; }
    }
}
