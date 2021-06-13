using Microsoft.EntityFrameworkCore.Migrations;

namespace Yarsey.EntityFramework.Migrations
{
    public partial class salesdependency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BusinessId",
                table: "Sales",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_BusinessId",
                table: "Sales",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Businesses_BusinessId",
                table: "Sales",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Businesses_BusinessId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_BusinessId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "BusinessId",
                table: "Sales");
        }
    }
}
