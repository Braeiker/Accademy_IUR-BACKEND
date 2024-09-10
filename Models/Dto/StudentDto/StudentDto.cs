namespace IUR_Backend.Models.Dto.StudentDto
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<StudentCourseDto> Courses { get; set; }
        public ICollection<StudentExamDto> Exams { get; set; }

    }
}
