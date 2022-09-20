using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevateOTT.Infrastructure.Migrations
{
    public partial class Migration1002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Language",
                newName: "LanguageCode",
                table: "ContentFeeds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
