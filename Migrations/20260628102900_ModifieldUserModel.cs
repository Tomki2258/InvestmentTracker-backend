using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestmentTracker_backend.Migrations
{
    /// <inheritdoc />
    public partial class ModifieldUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Surname",
                table: "users");
        }
    }
}
