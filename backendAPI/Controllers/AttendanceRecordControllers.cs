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
    public class AttendanceRecordController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRecordController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ADMIN: Get all attendance records
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var records = await _context.AttendanceRecords.Include(r => r.Student).ToListAsync();
            return Ok(records);
        }

        // TEACHER: Mark attendance
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> MarkAttendance(AttendanceRecord record)
        {
            _context.AttendanceRecords.Add(record);
            await _context.SaveChangesAsync();
            return Ok(record);
        }

        // TEACHER: Update attendance record
        [HttpPut("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Update(int id, AttendanceRecord record)
        {
            if (id != record.AttendanceRecordId) return BadRequest();

            _context.Entry(record).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Attendance record updated.");
        }

        // STUDENT/PARENT: View attendance records
        [HttpGet("student/{studentId}")]
        [Authorize(Roles = "Student,Parent")]
        public async Task<IActionResult> GetByStudentId(int studentId)
        {
            var records = await _context.AttendanceRecords
                .Where(r => r.StudentId == studentId)
                .ToListAsync();
            return Ok(records);
        }
    }
}
