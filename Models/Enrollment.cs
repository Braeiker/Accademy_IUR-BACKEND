namespace IUR_Backend.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
