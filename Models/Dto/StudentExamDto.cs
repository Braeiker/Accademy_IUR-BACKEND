namespace IUR_Backend.Models.Dto
{
    public class StudentExamDto
    {
        public int StudentExamId { get; set; }
        public string StudentId { get; set; }
        public int ExamId { get; set; }
        public int? Grade { get; set; }
    }
}
