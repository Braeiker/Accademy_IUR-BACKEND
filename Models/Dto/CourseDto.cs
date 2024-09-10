namespace IUR_Backend.Models.Dto
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TeacherId { get; set; }
        public TeacherDto Teacher { get; set; }
    }
}
