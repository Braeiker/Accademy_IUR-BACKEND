namespace IUR_Backend.Models.Dto
{
    public class CreateExamDto
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int CourseId { get; set; }
    }
}
