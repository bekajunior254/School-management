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
    public class GradeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GradeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ADMIN: Get all grades
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var grades = await _context.Grades.Include(g => g.Student).Include(g => g.Course).ToListAsync();
            return Ok(grades);
        }

        // ADMIN: Get grade by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var grade = await _context.Grades
                .Include(g => g.Student)
                .Include(g => g.Course)
                .FirstOrDefaultAsync(g => g.GradeId == id);

            if (grade == null) return NotFound();
            return Ok(grade);
        }

        // TEACHER: Assign or update grade
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AssignGrade(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return Ok(grade);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UpdateGrade(int id, Grade grade)
        {
            if (id != grade.GradeId) return BadRequest();

            _context.Entry(grade).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Grade updated successfully.");
        }

        // ADMIN: Delete grade
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) return NotFound();

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return Ok("Grade deleted successfully.");
        }

        // STUDENT: View own grades
        [HttpGet("me")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetOwnGrades()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == userId);
            if (student == null) return NotFound();

            var grades = await _context.Grades
                .Where(g => g.StudentId == student.StudentId)
                .Include(g => g.Course)
                .ToListAsync();

            return Ok(grades);
        }
    }
}
