using System.ComponentModel.DataAnnotations;

namespace School_Management_System.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        public required string Name { get; set; }  // e.g., "Form 1A", "Grade 6"

        public int? TeacherId { get; set; }

        // Navigation properties
        public Teacher? Teacher { get; set; }

        public ICollection<Student>? Students { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}
