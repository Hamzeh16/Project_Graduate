using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Graduates_Data.Migrations
{
    /// <inheritdoc />
    public partial class InsertRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "693c2c77-8688-482a-831b-81822da01f23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0a3dff9-6ba1-4433-8560-651dd74e1f45");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "09084478-51a6-4e75-b166-5f9f5e6f8632", "1", "Admin", "Admin" },
                    { "b6be7c34-b4a2-48c8-9a38-ad06113739c3", "2", "Company", "Company" },
                    { "dabadc51-85e5-4bbd-a155-1550f7490217", "3", "Student", "Student" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "09084478-51a6-4e75-b166-5f9f5e6f8632");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6be7c34-b4a2-48c8-9a38-ad06113739c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dabadc51-85e5-4bbd-a155-1550f7490217");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "693c2c77-8688-482a-831b-81822da01f23", "2", "Company", "Company" },
                    { "d0a3dff9-6ba1-4433-8560-651dd74e1f45", "1", "Admin", "Admin" }
                });
        }
    }
}
