using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Data;
using School_Management_System.Models;

namespace School_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ADMIN: Get all parents
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var parents = await _context.Parents.ToListAsync();
            return Ok(parents);
        }

        // ADMIN: Get parent by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null) return NotFound();
            return Ok(parent);
        }

        // ADMIN: Create parent
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Parent parent)
        {
            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();
            return Ok(parent);
        }

        // ADMIN: Update parent
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, Parent parent)
        {
            if (id != parent.ParentId) return BadRequest();

            _context.Entry(parent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Parent updated successfully.");
        }

        // ADMIN: Delete parent
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var parent = await _context.Parents.FindAsync(id);
            if (parent == null) return NotFound();

            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();
            return Ok("Parent deleted successfully.");
        }
    }
}
