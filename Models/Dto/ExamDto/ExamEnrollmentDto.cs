namespace IUR_Backend.Models.Dto.ExamDto
{
    public class ExamEnrollmentDto
    {
        public Guid StudentId { get; set; }
        public Guid ExamId { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
    }
}
