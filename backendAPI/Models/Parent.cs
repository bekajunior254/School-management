using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Management_System.Models
{
    public class Parent
    {
        [Key]
        public int ParentId { get; set; }

        [Required]
        public required string FullName { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        // Link to IdentityUser table (for login/auth)
        public string? UserId { get; set; }

        // Navigation property for linked students
        public ICollection<ParentStudentLink>? ChildrenLinks { get; set; }
    }
}
