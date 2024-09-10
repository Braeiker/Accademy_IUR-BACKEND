using IUR_Backend.Data;
using IUR_Backend.Models;
using IUR_Backend.Models.Dto.ExamDto;
using IUR_Backend.Models.Dto.StudentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IUR_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/teacher/exams
        [HttpPost("exams")]
        public async Task<IActionResult> CreateExam([FromBody] ExamDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Exam data is required.");
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Name == dto.CourseName);

            if (course == null)
            {
                return NotFound("Course not found.");
            }

            var exam = new Exam
            {
                Title = dto.Title,
                CourseId = course.CourseId
            };

            await _context.Exams.AddAsync(exam);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExamByName), new { exameName = exam.Title }, exam);
        }

        // PUT: api/teacher/exams
        [HttpPut("exams")]
        public async Task<IActionResult> UpdateExam([FromBody] ExamDto dto)
        {
            if (dto == null || dto.ExamId <= 0)
            {
                return BadRequest("Valid exam data is required.");
            }

            var exam = await _context.Exams
                .Include(e => e.StudentExams)
                .FirstOrDefaultAsync(e => e.ExamId == dto.ExamId);

            if (exam == null)
            {
                return NotFound("Exam not found.");
            }

            exam.Title = dto.Title;
            _context.Exams.Update(exam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/teacher/exams/grades
        [HttpPut("exams/grades")]
        public async Task<IActionResult> UpdateGrades([FromBody] ExamDto dto)
        {
            if (dto == null || dto.ExamId <= 0 || dto.StudentExams == null)
            {
                return BadRequest("Valid exam and student grades data are required.");
            }

            var exam = await _context.Exams
                .Include(e => e.StudentExams)
                .FirstOrDefaultAsync(e => e.ExamId == dto.ExamId);

            if (exam == null)
            {
                return NotFound("Exam not found.");
            }

            foreach (var studentExamDto in dto.StudentExams)
            {
                var studentExam = await _context.StudentExams
                    .FirstOrDefaultAsync(se => se.StudentId == studentExamDto.StudentId && se.ExamId == exam.ExamId);

                if (studentExam != null)
                {
                    studentExam.Grade = studentExamDto.Grade;
                    _context.StudentExams.Update(studentExam);
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/teacher/exams/{exameName}
        [HttpGet("exams/{exameName}")]
        public async Task<IActionResult> GetExamByName(string exameName)
        {
            if (string.IsNullOrWhiteSpace(exameName))
            {
                return BadRequest("Exam name is required.");
            }

            var exam = await _context.Exams
                .Include(e => e.Course)
                .Include(e => e.StudentExams)
                    .ThenInclude(se => se.Student)
                .FirstOrDefaultAsync(e => e.Title == exameName);

            if (exam == null)
            {
                return NotFound("Exam not found.");
            }

            var examDto = new ExamDto
            {
                ExamId = exam.ExamId,
                Title = exam.Title,
                CourseName = exam.Course?.Name,
                StudentExams = exam.StudentExams.Select(se => new StudentExamDto
                {
                    StudentId = se.StudentId,
                    Grade = se.Grade
                }).ToList()
            };

            return Ok(examDto);
        }
    }
}
