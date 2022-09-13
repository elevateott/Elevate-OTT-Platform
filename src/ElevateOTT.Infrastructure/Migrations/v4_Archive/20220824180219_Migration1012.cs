using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevateOTT.Infrastructure.Migrations
{
    public partial class Migration1012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasOneTimePurchasePrice",
                table: "Videos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasRentalPrice",
                table: "Videos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "OneTimePurchasePrice",
                table: "Videos",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RentalDuration",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "RentalPrice",
                table: "Videos",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasOneTimePurchasePrice",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "HasRentalPrice",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "OneTimePurchasePrice",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "RentalDuration",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "RentalPrice",
                table: "Videos");
        }
    }
}
