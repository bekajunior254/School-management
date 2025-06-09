namespace School_Management_System.DTOs
{
    public class TeacherDto
    {
        public int TeacherId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? SubjectSpecialization { get; set; }
        public string? UserId { get; set; }  // Link to IdentityUser
    }
}
