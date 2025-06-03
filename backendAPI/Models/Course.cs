using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public required string Name { get; set; }

        public int? TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher? Teacher { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
