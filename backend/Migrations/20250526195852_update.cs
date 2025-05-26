using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Context",
                table: "Blocks",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Blocks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Blocks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Blocks");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Blocks",
                newName: "Context");
        }
    }
}
