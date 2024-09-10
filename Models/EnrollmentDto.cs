using IUR_Backend.Models.Dto;

namespace IUR_Backend.Models
{
    public class EnrollmentDto
    {
        public int EnrollmentId { get; set; }
        public string StudentId { get; set; }
        public CourseDto Course { get; set; }
    }
}
