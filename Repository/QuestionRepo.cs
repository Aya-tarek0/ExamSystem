using ExamSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamSystem.Repository
{
    public class QuestionRepo : IQuestionRepo
    {
        private readonly ExamContext _context;

        public QuestionRepo(ExamContext context)
        {
            _context = context;
        }

        public async Task<Questions> AddAsync(Questions question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<Questions> GetByIdAsync(int id)
        {
            return await _context.Questions

               .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var question = await GetByIdAsync(id);
            if (question == null) 
                return false;

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Questions>> GetAllByExamAsync(int ExamId)
        {
            IEnumerable<Questions> questions = await _context.Questions.Where(e => e.ExamId == ExamId).ToListAsync();

            return questions;
        }


        public async Task<Questions> UpdateAsync(Questions question)
        {
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
            return question;
        }
    }
}
