using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevateOTT.Infrastructure.Migrations
{
    public partial class Migration1001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Permalink",
                table: "Authors",
                newName: "Slug");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Authors",
                newName: "Permalink");
        }
    }
}
