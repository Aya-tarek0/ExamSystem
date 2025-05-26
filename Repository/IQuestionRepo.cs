using ExamSystem.Models;

namespace ExamSystem.Repository
{
    public interface IQuestionRepo
    {
        Task<IEnumerable<Questions>> GetAllByExamAsync(int ExamId);
        Task<Questions> GetByIdAsync(int id);

        Task<Questions> AddAsync(Questions question);
        Task<Questions> UpdateAsync(Questions question);
        Task<bool> DeleteAsync(int id);
    }
}
