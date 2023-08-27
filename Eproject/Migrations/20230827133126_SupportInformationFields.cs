using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eproject.Migrations
{
    /// <inheritdoc />
    public partial class SupportInformationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "SupportInformation",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Company",
                table: "SupportInformation",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "SupportInformation",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "SupportInformation",
                newName: "Company");
        }
    }
}
