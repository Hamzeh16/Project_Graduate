using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Graduates_Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertDataRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "693c2c77-8688-482a-831b-81822da01f23", "2", "Company", "Company" },
                    { "d0a3dff9-6ba1-4433-8560-651dd74e1f45", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "693c2c77-8688-482a-831b-81822da01f23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0a3dff9-6ba1-4433-8560-651dd74e1f45");
        }
    }
}
