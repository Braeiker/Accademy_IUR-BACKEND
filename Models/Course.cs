using IUR_Backend.Settings;

namespace IUR_Backend.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<Exam> Exams { get; set; }

    }
}
