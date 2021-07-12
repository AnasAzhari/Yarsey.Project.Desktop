using Microsoft.EntityFrameworkCore.Migrations;

namespace Yarsey.EntityFramework.Migrations
{
    public partial class runningnoRemoveBusinessDependency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RunningNumbers_Businesses_business_id",
                table: "RunningNumbers");

            migrationBuilder.DropIndex(
                name: "IX_RunningNumbers_business_id",
                table: "RunningNumbers");

            migrationBuilder.DropColumn(
                name: "business_id",
                table: "RunningNumbers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "business_id",
                table: "RunningNumbers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RunningNumbers_business_id",
                table: "RunningNumbers",
                column: "business_id");

            migrationBuilder.AddForeignKey(
                name: "FK_RunningNumbers_Businesses_business_id",
                table: "RunningNumbers",
                column: "business_id",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
