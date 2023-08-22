using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eproject.Migrations
{
    /// <inheritdoc />
    public partial class SurveyAvalibility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Surveys",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Surveys");
        }
    }
}
