using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduates_Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldAppform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "emailCompany",
                table: "ApplicationForms",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "emailCompany",
                table: "ApplicationForms");
        }
    }
}
