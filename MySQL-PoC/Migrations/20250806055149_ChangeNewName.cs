using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySQL_PoC.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNewName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OriginalName",
                table: "Examples",
                newName: "NewName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewName",
                table: "Examples",
                newName: "OriginalName");
        }
    }
}
