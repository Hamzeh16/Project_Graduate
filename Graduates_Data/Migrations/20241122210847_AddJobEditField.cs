using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduates_Data.Migrations
{
    /// <inheritdoc />
    public partial class AddJobEditField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "STYDENTTYPE",
                table: "AspNetUsers",
                newName: "IMAGEURL");

            migrationBuilder.AddColumn<string>(
                name: "EmailTraining",
                table: "Traning",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Traning",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "applicDeadLine",
                table: "Traning",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "skillRequired",
                table: "Traning",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailJob",
                table: "Job",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Job",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "APPLICANTTYPE",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailTraining",
                table: "Traning");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Traning");

            migrationBuilder.DropColumn(
                name: "applicDeadLine",
                table: "Traning");

            migrationBuilder.DropColumn(
                name: "skillRequired",
                table: "Traning");

            migrationBuilder.DropColumn(
                name: "EmailJob",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "APPLICANTTYPE",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IMAGEURL",
                table: "AspNetUsers",
                newName: "STYDENTTYPE");
        }
    }
}
