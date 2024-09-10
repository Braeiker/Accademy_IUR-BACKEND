namespace IUR_Backend.Models
{
    public class Student : ApplicationUser
    {
        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }
    }
}
