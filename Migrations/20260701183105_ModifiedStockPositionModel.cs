using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestmentTracker_backend.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedStockPositionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stockPositions_users_UserId",
                table: "stockPositions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "stockPositions",
                newName: "PortfolioId");

            migrationBuilder.RenameIndex(
                name: "IX_stockPositions_UserId",
                table: "stockPositions",
                newName: "IX_stockPositions_PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_stockPositions_portfolios_PortfolioId",
                table: "stockPositions",
                column: "PortfolioId",
                principalTable: "portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stockPositions_portfolios_PortfolioId",
                table: "stockPositions");

            migrationBuilder.RenameColumn(
                name: "PortfolioId",
                table: "stockPositions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_stockPositions_PortfolioId",
                table: "stockPositions",
                newName: "IX_stockPositions_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_stockPositions_users_UserId",
                table: "stockPositions",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
