using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevateOTT.Infrastructure.Migrations
{
    public partial class Migration1014 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnimatedGifUrl",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CatalogImageUrl",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeaturedCatalogImageUrl",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayerImageUrl",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnimatedGifUrl",
                table: "Podcasts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CatalogImageUrl",
                table: "Podcasts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeaturedCatalogImageUrl",
                table: "Podcasts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayerImageUrl",
                table: "Podcasts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnimatedGifUrl",
                table: "LiveStreams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CatalogImageUrl",
                table: "LiveStreams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FeaturedCatalogImageUrl",
                table: "LiveStreams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayerImageUrl",
                table: "LiveStreams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.DropTable(
                name: "AssetImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimatedGifUrl",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CatalogImageUrl",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "FeaturedCatalogImageUrl",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "PlayerImageUrl",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "AnimatedGifUrl",
                table: "Podcasts");

            migrationBuilder.DropColumn(
                name: "CatalogImageUrl",
                table: "Podcasts");

            migrationBuilder.DropColumn(
                name: "FeaturedCatalogImageUrl",
                table: "Podcasts");

            migrationBuilder.DropColumn(
                name: "PlayerImageUrl",
                table: "Podcasts");

            migrationBuilder.DropColumn(
                name: "AnimatedGifUrl",
                table: "LiveStreams");

            migrationBuilder.DropColumn(
                name: "CatalogImageUrl",
                table: "LiveStreams");

            migrationBuilder.DropColumn(
                name: "FeaturedCatalogImageUrl",
                table: "LiveStreams");

            migrationBuilder.DropColumn(
                name: "PlayerImageUrl",
                table: "LiveStreams");
        }
    }
}
