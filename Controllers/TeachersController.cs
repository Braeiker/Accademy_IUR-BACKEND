using IUR_Backend.Models.Dto;
using IUR_Backend.Models.Dto.StudentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IUR_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Teacher")]
    public class TeacherController : ControllerBase
    {
        // Dummy data sources (in a real scenario, these would be services injected via DI)
        private static List<CourseDto> Courses = new List<CourseDto>();
        private static List<ExamDto> Exams = new List<ExamDto>();
        private static List<StudentExamDto> StudentExams = new List<StudentExamDto>();

        // POST: api/teacher/course
        [HttpPost("course")]
        public ActionResult CreateCourse([FromBody] CourseDto newCourse)
        {
            // Add the new course
            Courses.Add(newCourse);
            return Ok("Course created successfully.");
        }

        // PUT: api/teacher/course/{courseId}
        [HttpPut("course/{courseId}")]
        public ActionResult UpdateCourse(Guid courseId, [FromBody] CourseDto updatedCourse)
        {
            // Find the course to update
            var course = Courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                return NotFound("Course not found.");
            }

            // Update course details
            course.Name = updatedCourse.Name;
            course.Description = updatedCourse.Description;
            course.TeacherId = updatedCourse.TeacherId;

            return Ok("Course updated successfully.");
        }

        // DELETE: api/teacher/course/{courseId}
        [HttpDelete("course/{courseId}")]
        public ActionResult DeleteCourse(Guid courseId)
        {
            // Find the course to delete
            var course = Courses.FirstOrDefault(c => c.CourseId == courseId);
            if (course == null)
            {
                return NotFound("Course not found.");
            }

            Courses.Remove(course);
            return Ok("Course deleted successfully.");
        }

        // POST: api/teacher/exam
        [HttpPost("exam")]
        public ActionResult CreateExam([FromBody] ExamDto newExam)
        {
            // Add the new exam
            Exams.Add(newExam);
            return Ok("Exam created successfully.");
        }

        // PUT: api/teacher/exam/{examId}
        [HttpPut("exam/{examId}")]
        public ActionResult UpdateExam(Guid examId, [FromBody] ExamDto updatedExam)
        {
            // Find the exam to update
            var exam = Exams.FirstOrDefault(e => e.ExamId == examId);
            if (exam == null)
            {
                return NotFound("Exam not found.");
            }

            // Update exam details
            exam.Title = updatedExam.Title;
            exam.CourseName = updatedExam.CourseName;
            exam.StudentExams = updatedExam.StudentExams;

            return Ok("Exam updated successfully.");
        }

        // PUT: api/teacher/grade
        [HttpPut("grade")]
        public ActionResult AssignOrUpdateGrade([FromBody] StudentExamDto studentExam)
        {
            // Find the student exam record
            var existingStudentExam = StudentExams.FirstOrDefault(se => se.StudentExamId == studentExam.StudentExamId);
            if (existingStudentExam == null)
            {
                return NotFound("Student exam record not found.");
            }

            // Assign or update the grade
            existingStudentExam.Grade = studentExam.Grade;

            return Ok("Grade assigned/updated successfully.");
        }
    }
}
