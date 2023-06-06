using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementMvc.Migrations
{
    /// <inheritdoc />
    public partial class lppp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Students_StudentId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_StudentId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Managements");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Managements");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Chancellors");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Students",
                newName: "NextOfKin");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Students",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Lecturers",
                newName: "NextOfKin");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Lecturers",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Chancellors",
                newName: "NextOfKin");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Chancellors",
                newName: "Gender");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Students",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Lecturers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Chancellors",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Chancellors");

            migrationBuilder.RenameColumn(
                name: "NextOfKin",
                table: "Students",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Students",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "NextOfKin",
                table: "Lecturers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Lecturers",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "NextOfKin",
                table: "Chancellors",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Chancellors",
                newName: "Age");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "UserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Students",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Managements",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Managements",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Lecturers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Chancellors",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_StudentId",
                table: "UserRoles",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Students_StudentId",
                table: "UserRoles",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
