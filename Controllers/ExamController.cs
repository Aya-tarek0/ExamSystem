using System.Security.Claims;
using ExamSystem.DTO;
using ExamSystem.Models;
using ExamSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ExamSystem.Repository.IExamRepo;

namespace ExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamRepo _repo;
        private readonly IQuestionRepo questionRepo;
        private readonly IResultRepo resultRepo;
        private readonly IAnswerRepo answerRepo;

        public ExamController(IExamRepo repo ,IQuestionRepo questionRepo,
    IResultRepo resultRepo,
    IAnswerRepo answerRepo)
        {
            _repo = repo;
            this.questionRepo = questionRepo;
            this.resultRepo = resultRepo;
            this.answerRepo = answerRepo;
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetAllByUser()
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var exams = await _repo.GetAllByUserAsync(UserId);
            return Ok(exams);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var exam = await _repo.GetByIdAsync(id);
            if (exam == null) return NotFound();
            return Ok(exam);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Exam exam)
        {
            var UserID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            Exam newexam = new Exam()
            {
                UserId = UserID,
                CreatedAt = exam.CreatedAt,
                Title = exam.Title,
                Description = exam.Description,

            };
            var created = await _repo.AddAsync(newexam);
            return Ok(created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateExamDto dto)
        {
            var existingExam = await _repo.GetByIdAsync(id);
            if (existingExam == null)
                return NotFound();

            existingExam.Title = dto.Title;
            existingExam.Description = dto.Description;
          

            var updated = await _repo.UpdateAsync(existingExam);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var exams = await _repo.GetAllAsync();

            return Ok(exams);
        }


        [HttpPost("submit")]
        public async Task<IActionResult> SubmitExam([FromBody] SubmitExamDto dto)
        {
            var questions = await questionRepo.GetAllByExamAsync(dto.ExamId);

            int score = 0;
            var answers = new List<Answer>();
            var detailedAnswers = new List<object>(); 

            foreach (var question in questions)
            {
                var userAnswer = dto.Answers.FirstOrDefault(a => a.QuestionId == question.Id);
                if (userAnswer != null)
                {
                    bool isCorrect = question.CorrectAnswer == userAnswer.SelectedOption;
                    if (isCorrect)
                        score++;

                    answers.Add(new Answer
                    {
                        QuestionId = question.Id,
                        SelectedOption = userAnswer.SelectedOption
                    });

                    detailedAnswers.Add(new
                    {
                        questionText = question.Text,
                        selectedOption = userAnswer.SelectedOption,
                        correctOption = question.CorrectAnswer
                    });
                }
            }

            var result = new Result
            {
                ExamId = dto.ExamId,
                UserId = dto.UserId,
                Score = score,
                Answers = answers
            };

            await resultRepo.AddAsync(result);

            return Ok(new
            {
                message = "Exam submitted",
                score,
                total = questions.Count(),
                id = result.Id,
                answers = detailedAnswers
            });
        }

    }
}
