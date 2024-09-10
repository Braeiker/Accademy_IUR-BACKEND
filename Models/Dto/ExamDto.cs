namespace IUR_Backend.Models.Dto
{
    public class ExamDto
    {
        public int ExamId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int CourseId { get; set; }
        public CourseDto Course { get; set; }
        public ICollection<GradeDto> Grades { get; set; }
    }
}
