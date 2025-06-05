using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.Models
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; } = null!;

        [Required]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; } = null!;

        [Required]
        [StringLength(5)]
        public string GradeValue { get; set; } = string.Empty;  // e.g., "A", "B+", "85"

        [Required]
        public DateTime DateAwarded { get; set; } = DateTime.UtcNow;
    }
}
