using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Infrastructure.Persistence
{
    /// <inheritdoc />
    public partial class Updating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "UserTestResults");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "PracticeWorks");

            migrationBuilder.DropColumn(
                name: "DescriptionKz",
                table: "PracticeWorks");

            migrationBuilder.RenameColumn(
                name: "DescriptionRu",
                table: "PracticeWorks",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "PracticeWorks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Modules",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Modules",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.AddColumn<string>(
                name: "TaskPath",
                table: "Modules",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Courses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskPath",
                table: "Modules");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "PracticeWorks",
                newName: "DescriptionRu");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "PracticeWorks",
                type: "jsonb",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "PracticeWorks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionKz",
                table: "PracticeWorks",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Modules",
                type: "jsonb",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Modules",
                type: "jsonb",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Courses",
                type: "jsonb",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                type: "jsonb",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tests_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TestId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: true),
                    TitleKz = table.Column<string>(type: "text", nullable: true),
                    TitleRu = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTestResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TestId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestResults = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTestResults_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTestResults_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    TitleEn = table.Column<string>(type: "text", nullable: true),
                    TitleKz = table.Column<string>(type: "text", nullable: true),
                    TitleRu = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestId",
                table: "Questions",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_ModuleId",
                table: "Tests",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestResults_TestId",
                table: "UserTestResults",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTestResults_UserId",
                table: "UserTestResults",
                column: "UserId");
        }
    }
}
