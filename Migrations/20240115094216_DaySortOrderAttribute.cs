using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tweekClone.Migrations
{
    /// <inheritdoc />
    public partial class DaySortOrderAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DaySortOrder",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "DaySortOrder",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaySortOrder",
                table: "Items");
        }
    }
}
