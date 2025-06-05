public class AttendanceRecordDto
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public string Status { get; set; } = "Present"; // Present, Absent, Late
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
