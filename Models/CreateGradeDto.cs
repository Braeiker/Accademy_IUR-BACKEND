namespace IUR_Backend.Models
{
    public class CreateGradeDto
    {
        public int ExamId { get; set; }
        public string StudentId { get; set; }
        public double Score { get; set; }
    }
}
