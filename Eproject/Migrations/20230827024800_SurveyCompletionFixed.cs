using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eproject.Migrations
{
    /// <inheritdoc />
    public partial class SurveyCompletionFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarEntries_AspNetUsers_EprojectUserId",
                table: "SeminarEntries");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionTime",
                table: "SurveyCompletions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "EprojectUserId",
                table: "SeminarEntries",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarEntries_AspNetUsers_EprojectUserId",
                table: "SeminarEntries",
                column: "EprojectUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarEntries_AspNetUsers_EprojectUserId",
                table: "SeminarEntries");

            migrationBuilder.DropColumn(
                name: "CompletionTime",
                table: "SurveyCompletions");

            migrationBuilder.AlterColumn<string>(
                name: "EprojectUserId",
                table: "SeminarEntries",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarEntries_AspNetUsers_EprojectUserId",
                table: "SeminarEntries",
                column: "EprojectUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
