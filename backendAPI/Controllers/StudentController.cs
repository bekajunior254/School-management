using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Data;
using School_Management_System.Models;
using System.Security.Claims;
using System.Linq;

namespace School_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // TEACHER: View students in a teacher's courses
        [HttpGet("teacher/my-students")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetStudentsForTeacher()
        {
            var teacherUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (teacherUserId == null)
                return Unauthorized("Teacher user ID not found.");

            var courses = await _context.Courses
                .Where(c => c.Teacher != null && c.Teacher.UserId == teacherUserId)
                .Include(c => c.Enrollments!)
                    .ThenInclude(e => e.Student)
                .ToListAsync();

            var students = courses
                .SelectMany(c => c.Enrollments!.Select(e => e.Student))
                .Distinct()
                .ToList();

            return Ok(students);
        }

        // STUDENT: View own profile, courses, grades
        [HttpGet("me")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetOwnProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("User ID not found.");

            var student = await _context.Students
                .Include(s => s.Enrollments!)
                    .ThenInclude(e => e.Course)
                .Include(s => s.Grades)
                .FirstOrDefaultAsync(s => s.Email == userId); // Make sure userId maps to Email or change accordingly

            if (student == null)
                return NotFound("Student not found.");

            return Ok(student);
        }

        // Other controller methods...
    }
}
