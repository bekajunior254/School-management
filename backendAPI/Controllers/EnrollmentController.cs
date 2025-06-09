using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Data;
using School_Management_System.Models;

namespace School_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ADMIN: Get all enrollments
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var enrollments = await _context.Enrollments.Include(e => e.Student).Include(e => e.Course).ToListAsync();
            return Ok(enrollments);
        }

        // ADMIN: Get enrollment by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var enrollment = await _context.Enrollments.Include(e => e.Student).Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.EnrollmentId == id);

            if (enrollment == null) return NotFound();
            return Ok(enrollment);
        }

        // ADMIN: Create enrollment (assign student to course)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return Ok(enrollment);
        }

        // ADMIN: Update enrollment
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentId) return BadRequest();

            _context.Entry(enrollment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Enrollment updated successfully.");
        }

        // ADMIN: Delete enrollment
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null) return NotFound();

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return Ok("Enrollment deleted successfully.");
        }
    }
}
