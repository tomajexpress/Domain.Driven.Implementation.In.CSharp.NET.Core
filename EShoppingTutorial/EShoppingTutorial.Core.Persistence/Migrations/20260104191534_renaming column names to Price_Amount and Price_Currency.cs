using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShoppingTutorial.Core.Migrations
{
    /// <inheritdoc />
    public partial class renamingcolumnnamestoPrice_AmountandPrice_Currency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "OrderItems",
                newName: "Price_Currency");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "OrderItems",
                newName: "Price_Amount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price_Currency",
                table: "OrderItems",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "Price_Amount",
                table: "OrderItems",
                newName: "Amount");
        }
    }
}
