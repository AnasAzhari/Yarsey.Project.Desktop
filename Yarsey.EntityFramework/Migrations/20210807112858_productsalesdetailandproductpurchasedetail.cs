using Microsoft.EntityFrameworkCore.Migrations;

namespace Yarsey.EntityFramework.Migrations
{
    public partial class productsalesdetailandproductpurchasedetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Account_AssociatedAccountId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_AssociatedAccountId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AssociatedAccountId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCost",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductPurchaseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    PurchaseDescription = table.Column<string>(type: "TEXT", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPurchaseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPurchaseDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSalesDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    SalesDescription = table.Column<string>(type: "TEXT", nullable: true),
                    SalesPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSalesDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSalesDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductPurchaseDetails_ProductId",
                table: "ProductPurchaseDetails",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSalesDetails_ProductId",
                table: "ProductSalesDetails",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductPurchaseDetails");

            migrationBuilder.DropTable(
                name: "ProductSalesDetails");

            migrationBuilder.AddColumn<int>(
                name: "AssociatedAccountId",
                table: "Products",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ProductCost",
                table: "Products",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_AssociatedAccountId",
                table: "Products",
                column: "AssociatedAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Account_AssociatedAccountId",
                table: "Products",
                column: "AssociatedAccountId",
                principalTable: "Account",
                principalColumn: "Id");
        }
    }
}
