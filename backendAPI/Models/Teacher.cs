using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        public required string FullName { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        public string? SubjectSpecialization { get; set; }

        public ICollection<Class>? Classes { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}
