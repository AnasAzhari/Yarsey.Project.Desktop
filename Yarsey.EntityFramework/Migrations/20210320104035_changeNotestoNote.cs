using Microsoft.EntityFrameworkCore.Migrations;

namespace Yarsey.EntityFramework.Migrations
{
    public partial class changeNotestoNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Customers",
                newName: "Note");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Customers",
                newName: "Notes");
        }
    }
}
