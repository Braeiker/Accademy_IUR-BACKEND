using System.ComponentModel.DataAnnotations;

namespace IUR_Backend.Models
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public double Score { get; set; }
    }
}
