using ExamSystem.Models;
using Microsoft.EntityFrameworkCore;
using static ExamSystem.Repository.IExamRepo;

namespace ExamSystem.Repository
{
    public class ExamRepo : IExamRepo
    {
    
            private readonly ExamContext _context;

            public ExamRepo(ExamContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Exam>> GetAllAsync()
            {
                return await _context.exams.ToListAsync();
            }

        public async Task<IEnumerable<Exam>> GetAllByUserAsync(string userId)
        {
            return await _context.exams
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        public async Task<Exam> AddAsync(Exam exam)
            {
                exam.CreatedAt = DateTime.UtcNow;
                _context.exams.Add(exam);
                await _context.SaveChangesAsync();
                return exam;
            }

            public async Task<Exam> UpdateAsync(Exam exam)
            {
                _context.exams.Update(exam);
                await _context.SaveChangesAsync();
                return exam;
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var exam = await _context.exams.FindAsync(id);
                if (exam == null) return false;

                _context.exams.Remove(exam);
                await _context.SaveChangesAsync();
                return true;
            }

        public async Task<Exam> GetByIdAsync(int id)
        {
            return await _context.exams
                .Include(e => e.questions) 
                .FirstOrDefaultAsync(e => e.Id == id);
        }

    
    }

}

