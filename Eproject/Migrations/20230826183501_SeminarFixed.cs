using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eproject.Migrations
{
    /// <inheritdoc />
    public partial class SeminarFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarEntries_AspNetUsers_UserId",
                table: "SeminarEntries");

            migrationBuilder.DropIndex(
                name: "IX_SeminarEntries_UserId",
                table: "SeminarEntries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SeminarEntries");

            migrationBuilder.AlterColumn<string>(
                name: "PresentationMaterialLink",
                table: "SeminarEntries",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "EprojectUserId",
                table: "SeminarEntries",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Impact",
                table: "SeminarEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SeminarEntries_EprojectUserId",
                table: "SeminarEntries",
                column: "EprojectUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarEntries_AspNetUsers_EprojectUserId",
                table: "SeminarEntries",
                column: "EprojectUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarEntries_AspNetUsers_EprojectUserId",
                table: "SeminarEntries");

            migrationBuilder.DropIndex(
                name: "IX_SeminarEntries_EprojectUserId",
                table: "SeminarEntries");

            migrationBuilder.DropColumn(
                name: "EprojectUserId",
                table: "SeminarEntries");

            migrationBuilder.DropColumn(
                name: "Impact",
                table: "SeminarEntries");

            migrationBuilder.AlterColumn<string>(
                name: "PresentationMaterialLink",
                table: "SeminarEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SeminarEntries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeminarEntries_UserId",
                table: "SeminarEntries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarEntries_AspNetUsers_UserId",
                table: "SeminarEntries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
