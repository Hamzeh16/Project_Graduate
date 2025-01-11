using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduates_Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatJobField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailCompany",
                table: "Job",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailCompany",
                table: "Job");
        }
    }
}
