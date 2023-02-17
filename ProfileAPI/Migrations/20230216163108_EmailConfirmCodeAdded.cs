using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileAPI.Migrations
{
    /// <inheritdoc />
    public partial class EmailConfirmCodeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailConfirmCode",
                table: "Info",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmCode",
                table: "Info");
        }
    }
}
