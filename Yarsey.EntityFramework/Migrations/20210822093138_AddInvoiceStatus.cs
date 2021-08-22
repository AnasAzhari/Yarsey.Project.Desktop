using Microsoft.EntityFrameworkCore.Migrations;

namespace Yarsey.EntityFramework.Migrations
{
    public partial class AddInvoiceStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceStatus",
                table: "Invoices",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceStatus",
                table: "Invoices");
        }
    }
}
