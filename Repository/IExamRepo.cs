using ExamSystem.Models;

namespace ExamSystem.Repository
{
    public interface IExamRepo
    {
     
            Task<IEnumerable<Exam>> GetAllByUserAsync(string userId);
          Task<IEnumerable<Exam>> GetAllAsync();
        Task<Exam> GetByIdAsync(int id);
            Task<Exam> AddAsync(Exam exam);
            Task<Exam> UpdateAsync(Exam exam);
            Task<bool> DeleteAsync(int id);
        
    }
}
