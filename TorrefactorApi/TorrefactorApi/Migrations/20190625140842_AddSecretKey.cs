using Microsoft.EntityFrameworkCore.Migrations;

namespace TorrefactorApi.Migrations
{
    public partial class AddSecretKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationSecret",
                table: "Applications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationSecret",
                table: "Applications");
        }
    }
}
