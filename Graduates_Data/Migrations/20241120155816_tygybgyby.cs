using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Graduates_Data.Migrations
{
    /// <inheritdoc />
    public partial class tygybgyby : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "09084478-51a6-4e75-b166-5f9f5e6f8632");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "b6be7c34-b4a2-48c8-9a38-ad06113739c3");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "dabadc51-85e5-4bbd-a155-1550f7490217");

            //migrationBuilder.RenameColumn(
            //    name: "Email",
            //    table: "AspNetUsers",
            //    newName: "email");

            //migrationBuilder.AlterColumn<int>(
            //    name: "STYDENTTYPE",
            //    table: "AspNetUsers",
            //    type: "int",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<string>(
            //    name: "email",
            //    table: "AspNetUsers",
            //    type: "nvarchar(max)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(256)",
            //    oldMaxLength: 256,
            //    oldNullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "Email",
            //    table: "AspNetUsers",
            //    type: "nvarchar(256)",
            //    maxLength: 256,
            //    nullable: true);

            migrationBuilder.CreateTable(
                name: "Traning",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrainCost = table.Column<float>(type: "real", nullable: false),
                    TrainPeriod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traning", x => x.ID);
                });

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[,]
            //    {
            //        { "2d2de204-ba80-4823-9629-17de5f759cf5", "3", "Student", "Student" },
            //        { "53abb8aa-f19c-4258-9f5d-b860b96b50f2", "2", "Company", "Company" },
            //        { "da4105a5-0367-4836-b846-7882ab3d04d9", "1", "Admin", "Admin" }
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
