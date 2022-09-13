using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevateOTT.Infrastructure.Migrations
{
    public partial class MigrationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mp4Support = table.Column<bool>(type: "bit", nullable: false),
                    DownloadUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passthrough = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosedCaptions = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StreamUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaybackId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreamCreationStatus = table.Column<int>(type: "int", nullable: false),
                    PublicationStatus = table.Column<int>(type: "int", nullable: false),
                    IsTestAsset = table.Column<bool>(type: "bit", nullable: false),
                    IsHostedOnMux = table.Column<bool>(type: "bit", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    AllowDownload = table.Column<bool>(type: "bit", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrailerVideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlobUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentAccess = table.Column<int>(type: "int", nullable: false),
                    UploadedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReleasedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LanguageCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videos");
        }
    }
}
