using ExamSystem.DTO;
using ExamSystem.Models;
using ExamSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultRepo result;

        public ResultController(IResultRepo result)
        {
            this.result = result;
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var OneResult = await result.GetByIdAsync(id);

            ResultDto re = new ResultDto()
            {
                userName = OneResult.User.UserName,
                title = OneResult.Exam.Title,
                score = OneResult.Score,
                submitAt = OneResult.SubmittedAt,

            };
            return Ok(re);
        }


        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByUserID(string id)
        {
            var dtos = new List<ResultDto>();

            var results = await result.GetAllByUserAsync(id);

            foreach (var r in results)
            {
                dtos.Add(new ResultDto
                {
                    userName = r.User.UserName,
                    title = r.Exam.Title,
                    score = r.Score,
                    submitAt = r.SubmittedAt
                });
            }

            return Ok(dtos);
        }

        [HttpGet("Exam/{id}")]
        public async Task<IActionResult> GetByExammID(int id)
        {
            var dtos = new List<ResultDto>();

            var results = await result.GetAllByExamAsync(id);

            foreach (var r in results)
            {
                dtos.Add(new ResultDto
                {
                    userName = r.User.UserName,
                    title = r.Exam.Title,
                    score = r.Score,
                    submitAt = r.SubmittedAt
                });
            }

            return Ok(dtos);
        }


    }
}