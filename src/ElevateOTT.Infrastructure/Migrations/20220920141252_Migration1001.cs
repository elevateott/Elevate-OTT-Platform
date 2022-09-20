using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevateOTT.Infrastructure.Migrations
{
    public partial class Migration1001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SignedStreamUrl",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignedStreamUrl",
                table: "Podcasts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignedStreamUrl",
                table: "LiveStreams",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignedStreamUrl",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "SignedStreamUrl",
                table: "Podcasts");

            migrationBuilder.DropColumn(
                name: "SignedStreamUrl",
                table: "LiveStreams");
        }
    }
}
