using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Data;
using School_Management_System.Models;
using System.Security.Claims;

namespace School_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ADMIN: Get all teachers
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _context.Teachers.Include(t => t.Courses).ToListAsync();
            return Ok(teachers);
        }

        // ADMIN: Get teacher by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _context.Teachers
                .Include(t => t.Courses)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null) return NotFound();
            return Ok(teacher);
        }

        // ADMIN: Create new teacher
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return Ok(teacher);
        }

        // ADMIN: Update teacher details
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, Teacher teacher)
        {
            if (id != teacher.Id) return BadRequest();

            _context.Entry(teacher).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Teacher updated successfully.");
        }

        // ADMIN: Delete teacher
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return Ok("Teacher deleted successfully.");
        }

        // TEACHER: View own courses
        [HttpGet("me/courses")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetOwnCourses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var teacher = await _context.Teachers.Include(t => t.Courses)
                .FirstOrDefaultAsync(t => t.UserId == userId);

            if (teacher == null) return NotFound("Teacher not found");

            return Ok(teacher.Courses);
        }

        // TEACHER: View students enrolled in their courses
        [HttpGet("me/students")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetEnrolledStudents()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var teacher = await _context.Teachers.Include(t => t.Courses)
                .FirstOrDefaultAsync(t => t.UserId == userId);

            if (teacher == null) return NotFound("Teacher not found");

            // Get all courses taught by teacher
            var courseIds = teacher.Courses!.Select(c => c.CourseId).ToList();

            // Get students enrolled in those courses
            var students = await _context.Enrollments
                .Where(e => courseIds.Contains(e.CourseId))
                .Include(e => e.Student)
                .Select(e => e.Student)
                .Distinct()
                .ToListAsync();

            return Ok(students);
        }
    }
}
