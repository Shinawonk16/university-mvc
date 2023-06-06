using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityManagementMvc.Migrations
{
    /// <inheritdoc />
    public partial class mitrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LecturerCourse_Courses_CourseId",
                table: "LecturerCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_LecturerCourse_Lecturers_LecturerId",
                table: "LecturerCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LecturerCourse",
                table: "LecturerCourse");

            migrationBuilder.RenameTable(
                name: "LecturerCourse",
                newName: "LecturerCourses");

            migrationBuilder.RenameIndex(
                name: "IX_LecturerCourse_LecturerId",
                table: "LecturerCourses",
                newName: "IX_LecturerCourses_LecturerId");

            migrationBuilder.RenameIndex(
                name: "IX_LecturerCourse_CourseId",
                table: "LecturerCourses",
                newName: "IX_LecturerCourses_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LecturerCourses",
                table: "LecturerCourses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "StudentCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_CourseId",
                table: "StudentCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_StudentId",
                table: "StudentCourses",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerCourses_Courses_CourseId",
                table: "LecturerCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerCourses_Lecturers_LecturerId",
                table: "LecturerCourses",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LecturerCourses_Courses_CourseId",
                table: "LecturerCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_LecturerCourses_Lecturers_LecturerId",
                table: "LecturerCourses");

            migrationBuilder.DropTable(
                name: "StudentCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LecturerCourses",
                table: "LecturerCourses");

            migrationBuilder.RenameTable(
                name: "LecturerCourses",
                newName: "LecturerCourse");

            migrationBuilder.RenameIndex(
                name: "IX_LecturerCourses_LecturerId",
                table: "LecturerCourse",
                newName: "IX_LecturerCourse_LecturerId");

            migrationBuilder.RenameIndex(
                name: "IX_LecturerCourses_CourseId",
                table: "LecturerCourse",
                newName: "IX_LecturerCourse_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LecturerCourse",
                table: "LecturerCourse",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerCourse_Courses_CourseId",
                table: "LecturerCourse",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerCourse_Lecturers_LecturerId",
                table: "LecturerCourse",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
