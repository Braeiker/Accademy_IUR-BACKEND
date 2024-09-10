namespace IUR_Backend.Models.Dto.StudentDto
{
    public class StudentCourseDto
    {
        public Guid StudentCourseId { get; set; }
        public Guid StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
