using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backendAPI.Migrations
{
    /// <inheritdoc />
    public partial class Iupdateemail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Enrollments",
                newName: "EnrollmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnrollmentId",
                table: "Enrollments",
                newName: "Id");
        }
    }
}
