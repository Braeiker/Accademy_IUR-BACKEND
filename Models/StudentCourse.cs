using System.ComponentModel.DataAnnotations;

namespace IUR_Backend.Models
{
    public class StudentCourse
    {
        [Key]
        public int StudentCourseId { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
