namespace IUR_Backend.Models.Dto
{
    public class TeacherDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<CourseDto> Courses { get; set; }
    }
}
