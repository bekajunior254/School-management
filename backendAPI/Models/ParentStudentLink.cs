using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.Models
{
    public class ParentStudentLink
    {
        [Key]
        public int ParentStudentLinkId { get; set; }

        [Required]
        public int ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Parent Parent { get; set; } = null!;

        [Required]
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; } = null!;
    }
}
