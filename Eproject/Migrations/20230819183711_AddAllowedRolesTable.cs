using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eproject.Migrations
{
    /// <inheritdoc />
    public partial class AddAllowedRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllowedRole_Surveys_SurveyId",
                table: "AllowedRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllowedRole",
                table: "AllowedRole");

            migrationBuilder.RenameTable(
                name: "AllowedRole",
                newName: "AllowedRoles");

            migrationBuilder.RenameIndex(
                name: "IX_AllowedRole_SurveyId",
                table: "AllowedRoles",
                newName: "IX_AllowedRoles_SurveyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllowedRoles",
                table: "AllowedRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllowedRoles_Surveys_SurveyId",
                table: "AllowedRoles",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllowedRoles_Surveys_SurveyId",
                table: "AllowedRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllowedRoles",
                table: "AllowedRoles");

            migrationBuilder.RenameTable(
                name: "AllowedRoles",
                newName: "AllowedRole");

            migrationBuilder.RenameIndex(
                name: "IX_AllowedRoles_SurveyId",
                table: "AllowedRole",
                newName: "IX_AllowedRole_SurveyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllowedRole",
                table: "AllowedRole",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllowedRole_Surveys_SurveyId",
                table: "AllowedRole",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
