using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eproject.Migrations
{
    /// <inheritdoc />
    public partial class SurveyCompletins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyCompletion_AspNetUsers_EprojectUserId",
                table: "SurveyCompletion");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyCompletion_Surveys_SurveyId",
                table: "SurveyCompletion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyCompletion",
                table: "SurveyCompletion");

            migrationBuilder.RenameTable(
                name: "SurveyCompletion",
                newName: "SurveyCompletions");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyCompletion_SurveyId",
                table: "SurveyCompletions",
                newName: "IX_SurveyCompletions_SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyCompletion_EprojectUserId",
                table: "SurveyCompletions",
                newName: "IX_SurveyCompletions_EprojectUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyCompletions",
                table: "SurveyCompletions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyCompletions_AspNetUsers_EprojectUserId",
                table: "SurveyCompletions",
                column: "EprojectUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyCompletions_Surveys_SurveyId",
                table: "SurveyCompletions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyCompletions_AspNetUsers_EprojectUserId",
                table: "SurveyCompletions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyCompletions_Surveys_SurveyId",
                table: "SurveyCompletions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyCompletions",
                table: "SurveyCompletions");

            migrationBuilder.RenameTable(
                name: "SurveyCompletions",
                newName: "SurveyCompletion");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyCompletions_SurveyId",
                table: "SurveyCompletion",
                newName: "IX_SurveyCompletion_SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyCompletions_EprojectUserId",
                table: "SurveyCompletion",
                newName: "IX_SurveyCompletion_EprojectUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyCompletion",
                table: "SurveyCompletion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyCompletion_AspNetUsers_EprojectUserId",
                table: "SurveyCompletion",
                column: "EprojectUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyCompletion_Surveys_SurveyId",
                table: "SurveyCompletion",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
