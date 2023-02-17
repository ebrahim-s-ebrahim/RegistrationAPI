using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileAPI.Migrations
{
    /// <inheritdoc />
    public partial class EmailCheckedAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailIsChecked",
                table: "Info",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailIsChecked",
                table: "Info");
        }
    }
}
