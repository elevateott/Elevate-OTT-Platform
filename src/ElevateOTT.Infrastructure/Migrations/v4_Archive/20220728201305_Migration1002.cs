using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevateOTT.Infrastructure.Migrations
{
    public partial class Migration1002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomDomain",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomDomain",
                table: "Tenants");
        }
    }
}
