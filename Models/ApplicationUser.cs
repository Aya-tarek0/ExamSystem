using ExamSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace Quiz.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ICollection<Result> Results { get; set; }
        public ICollection<Exam> Exams { get; set; }


    }
}
