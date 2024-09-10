namespace IUR_Backend.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }

    }
}
