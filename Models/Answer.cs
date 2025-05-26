namespace ExamSystem.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }
        public Questions Question { get; set; }

        public string SelectedOption { get; set; }

        public int ResultId { get; set; }
        public Result Result { get; set; }
    }
}
