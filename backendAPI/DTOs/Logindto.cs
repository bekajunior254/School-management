namespace School_Management_System.DTOs
{
    public class LoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
         public required string Role { get; set; }
    }
}
