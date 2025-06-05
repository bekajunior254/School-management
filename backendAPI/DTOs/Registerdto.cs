namespace School_Management_System.DTOs
{
    public class RegisterDto
    {
        public required string Username { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
         public required string Role { get; set; } = string.Empty;
    }
}
