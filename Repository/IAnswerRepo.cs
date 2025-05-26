using ExamSystem.Models;

namespace ExamSystem.Repository
{
    public interface IAnswerRepo
    {
        Task AddRangeAsync(IEnumerable<Answer> answers);

    }
}
