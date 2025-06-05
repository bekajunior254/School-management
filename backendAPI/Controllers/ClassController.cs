using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Data;
using School_Management_System.Models;

namespace School_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ClassController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ADMIN: Get all classes
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var classes = await _context.Classes.ToListAsync();
            return Ok(classes);
        }

        // ADMIN: Get class by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var cls = await _context.Classes.FindAsync(id);
            if (cls == null) return NotFound();
            return Ok(cls);
        }

        // ADMIN: Create new class
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Class cls)
        {
            _context.Classes.Add(cls);
            await _context.SaveChangesAsync();
            return Ok(cls);
        }

        // ADMIN: Update class
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, Class cls)
        {
            if (id != cls.ClassId) return BadRequest();

            _context.Entry(cls).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Class updated successfully.");
        }

        // ADMIN: Delete class
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var cls = await _context.Classes.FindAsync(id);
            if (cls == null) return NotFound();

            _context.Classes.Remove(cls);
            await _context.SaveChangesAsync();
            return Ok("Class deleted successfully.");
        }
    }
}
