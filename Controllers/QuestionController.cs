using System.Security.Claims;
using ExamSystem.DTO;
using ExamSystem.Models;
using ExamSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepo _repo;

        public QuestionController(IQuestionRepo repo)
        {
            _repo = repo;

        }

        [HttpGet("/question/{id}")]
        public async Task<IActionResult> getById(int id)
        {
            Questions questions = await _repo.GetByIdAsync(id);

            return Ok(questions);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllByExam(int ExamId)
        {

            var questions = await _repo.GetAllByExamAsync(ExamId);
            return Ok(questions);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Questions question)
        {
          
            var created = await _repo.AddAsync(question);
            return Ok(created);
        }

        [HttpPut("{id}")]
        

        public async Task<IActionResult> Update(int id, [FromBody] questionDto question)
        {
            var existingQuestion = await _repo.GetByIdAsync(id);
            if (existingQuestion == null)
                return NotFound();

            existingQuestion.Text = question.Text;
            existingQuestion.OptionA = question.OptionA;
            existingQuestion.OptionB = question.OptionB;
            existingQuestion.OptionC = question.OptionC;
            existingQuestion.OptionD = question.OptionD;
            existingQuestion.CorrectAnswer = question.CorrectAnswer;




            var updated = await _repo.UpdateAsync(existingQuestion);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
