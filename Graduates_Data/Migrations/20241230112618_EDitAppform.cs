using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduates_Data.Migrations
{
    /// <inheritdoc />
    public partial class EDitAppform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YourName",
                table: "ApplicationForms",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "YourEmail",
                table: "ApplicationForms",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "ApplicationForms",
                newName: "email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phone",
                table: "ApplicationForms",
                newName: "YourName");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ApplicationForms",
                newName: "YourEmail");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "ApplicationForms",
                newName: "PhoneNumber");
        }
    }
}
