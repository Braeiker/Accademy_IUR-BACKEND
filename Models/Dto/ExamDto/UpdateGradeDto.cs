using System.ComponentModel.DataAnnotations;

namespace IUR_Backend.Models.Dto.ExamDto
{
    public class UpdateGradeDto
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int ExamId { get; set; }
        [Required]
        public int Grade { get; set; }
    }
}
