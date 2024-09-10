using System.ComponentModel.DataAnnotations;
using IUR_Backend.Settings;
using Microsoft.AspNetCore.Identity;

namespace IUR_Backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
