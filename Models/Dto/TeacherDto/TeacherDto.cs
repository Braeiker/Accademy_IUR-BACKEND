namespace IUR_Backend.Models.Dto.TeacherDto
{
    using IUR_Backend.Models.Dto.CourseDto;

    public class TeacherDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<CourseDto> Courses { get; set; }
    }
}
