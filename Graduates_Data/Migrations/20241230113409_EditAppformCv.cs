using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduates_Data.Migrations
{
    /// <inheritdoc />
    public partial class EditAppformCv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "ApplicationForms",
                newName: "cv");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cv",
                table: "ApplicationForms",
                newName: "ImageUrl");
        }
    }
}
