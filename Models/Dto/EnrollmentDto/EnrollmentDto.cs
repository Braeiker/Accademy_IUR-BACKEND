namespace IUR_Backend.Models.Dto
{
    public class EnrollmentDto
    {
        public Guid EnrollmentId { get; set; }

        public string StudentId { get; set; }

        public string StudentEmail { get; set; }
        public string CourseName { get; set; }

        public Guid CourseId { get; set; }

    }
}
