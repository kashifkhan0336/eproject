using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eproject.Migrations
{
    /// <inheritdoc />
    public partial class ManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Surveys_SurveyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SurveyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "SurveyEprojectUsers",
                columns: table => new
                {
                    SurveyId = table.Column<int>(type: "int", nullable: false),
                    EprojectUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyEprojectUsers", x => new { x.SurveyId, x.EprojectUserId });
                    table.ForeignKey(
                        name: "FK_SurveyEprojectUsers_AspNetUsers_EprojectUserId",
                        column: x => x.EprojectUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyEprojectUsers_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyEprojectUsers_EprojectUserId",
                table: "SurveyEprojectUsers",
                column: "EprojectUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyEprojectUsers");

            migrationBuilder.AddColumn<int>(
                name: "SurveyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SurveyId",
                table: "AspNetUsers",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Surveys_SurveyId",
                table: "AspNetUsers",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id");
        }
    }
}
