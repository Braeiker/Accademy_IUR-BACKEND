namespace IUR_Backend.Models.Dto.StudentDto
{
    public class StudentExamDto

    {
        public int StudentExamId { get; set; }
        public string StudentId { get; set; }
        public int ExamId { get; set; }
        public double? Grade { get; set; }
    }
}
