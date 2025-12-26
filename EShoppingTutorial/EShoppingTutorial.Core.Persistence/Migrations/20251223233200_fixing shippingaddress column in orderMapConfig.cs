using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShoppingTutorial.Core.Migrations
{
    /// <inheritdoc />
    public partial class fixingshippingaddresscolumninorderMapConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingAdress",
                table: "Orders",
                newName: "ShippingAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShippingAddress",
                table: "Orders",
                newName: "ShippingAdress");
        }
    }
}
