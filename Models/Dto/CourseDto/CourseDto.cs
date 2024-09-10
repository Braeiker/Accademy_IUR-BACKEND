
namespace IUR_Backend.Models.Dto.CourseDto

{
    using IUR_Backend.Models.Dto.TeacherDto;

    public class CourseDto
    {
        public Guid CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid TeacherId { get; set; }
        public TeacherDto Teacher { get; set; }
    }
}
