using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevateOTT.Infrastructure.Migrations
{
    public partial class Migration1016 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tenants_Name",
                table: "Tenants");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tenants",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "OttChannelName",
                table: "Tenants",
                newName: "ChannelName");

            migrationBuilder.AddColumn<string>(
                name: "HeardAboutUsFrom",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubDomain",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_FullName",
                table: "Tenants",
                column: "FullName",
                unique: true,
                filter: "[FullName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tenants_FullName",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "HeardAboutUsFrom",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "SubDomain",
                table: "Tenants");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Tenants",
                newName: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Name",
                table: "Tenants",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
