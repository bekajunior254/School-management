public class GradeDto
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public string GradeValue { get; set; } = string.Empty;
    public DateTime DateAwarded { get; set; } = DateTime.UtcNow;
}
