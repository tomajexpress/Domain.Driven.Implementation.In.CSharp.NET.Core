using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShoppingTutorial.Core.Migrations
{
    /// <inheritdoc />
    public partial class renamgingMoneyUnittoCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "OrderItems",
                newName: "Currency");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "OrderItems",
                newName: "Unit");
        }
    }
}
