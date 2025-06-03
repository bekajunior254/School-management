using System.ComponentModel.DataAnnotations;

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

    }
}
