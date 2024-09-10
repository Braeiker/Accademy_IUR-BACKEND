namespace IUR_Backend.Models
{
    public class StudentExam
    {
        public int StudentExamId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public double? Grade { get; set; }
    }
}
