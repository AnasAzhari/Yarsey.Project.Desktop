using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Yarsey.EntityFramework.Migrations
{
    public partial class deletelogged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logged",
                table: "Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Logged",
                table: "Customers",
                type: "TEXT",
                nullable: true);
        }
    }
}
