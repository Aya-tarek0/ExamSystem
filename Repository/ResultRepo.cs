using ExamSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamSystem.Repository
{
    public class ResultRepo :IResultRepo
    {
        private readonly ExamContext _context;

        public ResultRepo(ExamContext context)
        {
            _context = context;
        }

        public async Task<Result> AddAsync(Result result)
        {
            _context.results.Add(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Result> GetByIdAsync(int id)
        {
            return await _context.results.Include(r => r.Answers).Include(e => e.User).Include(e => e.Exam).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Result>> GetAllByUserAsync(string userId)
        {
            return await _context.results.Where(r => r.UserId == userId).Include(e=>e.User).Include(e=>e.Exam).ToListAsync();
        }

        public async Task<IEnumerable<Result>> GetAllByExamAsync(int Id)
        {
            return await _context.results.Where(r => r.ExamId == Id).Include(e => e.User).Include(e => e.Exam).ToListAsync();
        }
    }
}
