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

        public int? ClassId { get; set; }

        [ForeignKey("ClassId")]
        public Class? Class { get; set; }


        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
