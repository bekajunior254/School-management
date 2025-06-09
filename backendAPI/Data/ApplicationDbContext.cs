using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using School_Management_System.Models;

namespace School_Management_System.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<ParentStudentLink> ParentStudentLinks { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define Teacher - Course relationship
            builder.Entity<Teacher>()
                .HasMany(t => t.Courses)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define Parent - Student link
            builder.Entity<ParentStudentLink>()
                .HasKey(ps => new { ps.ParentId, ps.StudentId });

            builder.Entity<ParentStudentLink>()
                .HasOne(ps => ps.Parent)
                .WithMany(p => p.ChildrenLinks)
                .HasForeignKey(ps => ps.ParentId);

            builder.Entity<ParentStudentLink>()
                .HasOne(ps => ps.Student)
                .WithMany(s => s.ParentLinks)
                .HasForeignKey(ps => ps.StudentId);
        }
    }
}