using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduates_Data.Migrations
{
    /// <inheritdoc />
    public partial class aere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "duration",
                table: "Job",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "internshipType",
                table: "Job",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "duration",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "internshipType",
                table: "Job");
        }
    }
}
