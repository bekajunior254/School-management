using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        public required string FullName { get; set; }

        public string? Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        // Link to IdentityUser.Id for authentication mapping
        public string? UserId { get; set; }

        public int? ClassId { get; set; }

        [ForeignKey("ClassId")]
        public Class? Class { get; set; }

        // Navigation property for course enrollments
        public ICollection<Enrollment>? Enrollments { get; set; }

        // Navigation property for grades
        public ICollection<Grade>? Grades { get; set; }

        // Navigation property for attendance records
        public ICollection<AttendanceRecord>? AttendanceRecords { get; set; }

        // Helper property (not mapped to DB) to easily get courses from enrollments
        [NotMapped]
        public IEnumerable<Course> Courses => Enrollments?.Select(e => e.Course) ?? Enumerable.Empty<Course>();
    }
}
