using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevateOTT.Infrastructure.Migrations
{
    public partial class Migration1000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "ContentFeeds");

            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "ContentFeeds",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ContentFeeds");

            migrationBuilder.AddColumn<string>(
                name: "LastUpdated",
                table: "ContentFeeds",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
