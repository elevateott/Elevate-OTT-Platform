using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevateOTT.Infrastructure.Migrations
{
    public partial class Migration1003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreamUrl",
                newName: "PublicStreamUrl",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "StreamUrl",
                newName: "PublicStreamUrl",
                table: "LiveStreams");

            migrationBuilder.RenameColumn(
                name: "StreamUrl",
                newName: "PublicStreamUrl",
                table: "Podcasts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
