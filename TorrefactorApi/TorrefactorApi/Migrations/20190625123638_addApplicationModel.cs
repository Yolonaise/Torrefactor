using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TorrefactorApi.Migrations
{
    public partial class addApplicationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApiKey = table.Column<string>(nullable: true),
                    ApplicationName = table.Column<string>(nullable: true),
                    ApplicationDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");
        }
    }
}
