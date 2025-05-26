namespace ExamSystem.DTO
{
    public class SubmitExamDto
    {
        public string UserId { get; set; }
        public int ExamId { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
