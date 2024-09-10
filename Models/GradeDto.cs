using IUR_Backend.Models.Dto;

namespace IUR_Backend.Models
{
    public class GradeDto
    {
        public int GradeId { get; set; }
        public int ExamId { get; set; }
        public ExamDto Exam { get; set; }
        public string StudentId { get; set; }
        public StudentDto Student { get; set; }
        public double Score { get; set; }
    }
}
