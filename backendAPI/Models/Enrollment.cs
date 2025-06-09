using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public  required Student Student { get; set; }

        [Required]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public required Course Course { get; set; }

        public DateTime EnrolledOn { get; set; } = DateTime.UtcNow;
    }
}
