using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Data;
using School_Management_System.DTOs;
using School_Management_System.Models;

namespace School_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParentStudentLinkController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParentStudentLinkController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ADMIN: Get all links
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllLinks()
        {
            var links = await _context.ParentStudentLinks
                .Include(p => p.Parent)
                .Include(s => s.Student)
                .ToListAsync();

            return Ok(links.Select(link => new ParentStudentLinkDto
            {
                ParentStudentLinkId = link.ParentStudentLinkId,
                ParentId = link.ParentId,
                StudentId = link.StudentId
            }));
        }

        // ADMIN: Link a parent to a student
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> LinkParentToStudent(ParentStudentLinkDto dto)
        {
            var exists = await _context.ParentStudentLinks
                .AnyAsync(l => l.ParentId == dto.ParentId && l.StudentId == dto.StudentId);

            if (exists)
                return BadRequest("Link already exists.");

            var link = new ParentStudentLink
            {
                ParentId = dto.ParentId,
                StudentId = dto.StudentId
            };

            _context.ParentStudentLinks.Add(link);
            await _context.SaveChangesAsync();
            return Ok(link);
        }

        // ADMIN: Delete a parent-student link
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteLink(int id)
        {
            var link = await _context.ParentStudentLinks.FindAsync(id);
            if (link == null) return NotFound();

            _context.ParentStudentLinks.Remove(link);
            await _context.SaveChangesAsync();

            return Ok("Link deleted successfully.");
        }
    }
}
