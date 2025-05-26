using ExamSystem.Models;

namespace ExamSystem.Repository
{
    public class AnswerRepo:IAnswerRepo
    {
        private readonly ExamContext _context;

        public AnswerRepo(ExamContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<Answer> answers)
        {
            _context.answers.AddRange(answers);
            await _context.SaveChangesAsync();
        }
    }
}
