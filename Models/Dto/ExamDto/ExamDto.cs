using IUR_Backend.Models.Dto.StudentDto;

namespace IUR_Backend.Models.Dto.ExamDto
{
    public class ExamDto
    {
        public Guid ExamId { get; set; }
        public string Title { get; set; }
        public string CourseName { get; set; }

        public ICollection<StudentExamDto> StudentExams { get; set; }
    }
}
