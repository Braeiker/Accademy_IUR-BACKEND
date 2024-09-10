using IUR_Backend.Models.Dto.CourseDto;
using IUR_Backend.Models.Dto.ExamDto;
using IUR_Backend.Models.Dto.StudentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IUR_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Student")]
    public class StudentController : ControllerBase
    {
        private static List<StudentDto> Students = new List<StudentDto>();
        private static List<ExamDto> Exams = new List<ExamDto>();
        private static List<StudentExamDto> StudentExams = new List<StudentExamDto>();
        private static List<StudentCourseDto> StudentCourses = new List<StudentCourseDto>();
        private static List<EnrollmentDto> Enrollments = new List<EnrollmentDto>();
        private static List<CourseDto> Courses = new List<CourseDto>();

        // GET: api/student/exams
        [HttpGet("exams")]
        public ActionResult<IEnumerable<ExamDto>> GetAllExams()
        {
            // Retrieves all exams
            return Ok(Exams);
        }
        [HttpGet("course")]
        public ActionResult<IEnumerable<CourseDto>> GetCourseByName([FromQuery] string name)
        {
            // Retrieves courses by name
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Course name is required.");
            }

            var matchingCourses = Courses.Where(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!matchingCourses.Any())
            {
                return NotFound($"No courses found with the name: {name}");
            }

            return Ok(matchingCourses);
        }

        // GET: api/student/courses
        [HttpGet("courses")]
        public ActionResult<IEnumerable<CourseDto>> GetAllCourses()
        {
            // Retrieves all courses
            return Ok(Courses);
        }

        // POST: api/student/enroll/course
        [HttpPost("enroll/course")]
        public ActionResult EnrollInCourse([FromBody] EnrollmentDto enrollment)
        {
            // Check if the student is already enrolled in the course
            if (Enrollments.Any(e => e.StudentId == enrollment.StudentId && e.CourseId == enrollment.CourseId))
            {
                return BadRequest("Student is already enrolled in this course.");
            }

            // Enroll the student in the course
            Enrollments.Add(enrollment);
            return Ok("Enrollment successful.");
        }

        // POST: api/student/enroll/exam
        [HttpPost("enroll/exam")]
        public ActionResult EnrollInExam([FromBody] ExamEnrollmentDto examEnrollment)
        {
            // Check if the student is already enrolled in the exam
            if (StudentExams.Any(e => e.StudentId == examEnrollment.StudentId.ToString() && e.ExamId == examEnrollment.ExamId.GetHashCode()))
            {
                return BadRequest("Student is already enrolled in this exam.");
            }

            // Enroll the student in the exam
            StudentExams.Add(new StudentExamDto
            {
                StudentExamId = StudentExams.Count + 1,
                StudentId = examEnrollment.StudentId.ToString(),
                ExamId = examEnrollment.ExamId.GetHashCode(),
                Grade = null
            });

            return Ok("Exam enrollment successful.");
        }

        // GET: api/student/grades/{studentId}
        [HttpGet("grades/{studentId}")]
        public ActionResult<IEnumerable<StudentExamDto>> GetStudentGrades(Guid studentId)
        {
            // Retrieves the grades for the specified student
            var grades = StudentExams.Where(se => se.StudentId == studentId.ToString()).ToList();
            if (!grades.Any())
            {
                return NotFound("No grades found for this student.");
            }

            return Ok(grades);
        }
    }
}