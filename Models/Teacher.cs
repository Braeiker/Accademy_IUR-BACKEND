using IUR_Backend.Models;

namespace IUR_Backend.Settings
{
    public class Teacher : ApplicationUser
    {
        public ICollection<Course> Courses { get; set; }
    }
}
