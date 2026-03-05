using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierNumber",
                table: "Products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierNumber",
                table: "Products",
                column: "SupplierNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_SupplierNumber",
                table: "Products",
                column: "SupplierNumber",
                principalTable: "Suppliers",
                principalColumn: "SupplierNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_SupplierNumber",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SupplierNumber",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SupplierNumber",
                table: "Products");
        }
    }
}
