using ExamSystem.Models;

namespace ExamSystem.Repository
{
    public interface IResultRepo
    {
        Task<Result> AddAsync(Result result);
        Task<Result> GetByIdAsync(int id);
        Task<IEnumerable<Result>> GetAllByUserAsync(string userId);
        Task<IEnumerable<Result>> GetAllByExamAsync(int Id);
    }
}
