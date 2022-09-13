using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevateOTT.Infrastructure.Migrations
{
    public partial class Migration1015 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VideosTags");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "VideosTags");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "VideosTags");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "VideosTags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VideosTags");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "VideosTags");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "VideosTags");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VideosCollections");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "VideosCollections");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "VideosCollections");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "VideosCollections");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VideosCollections");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "VideosCollections");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "VideosCollections");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "VideosCategories");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "VideosCategories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "VideosCategories");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "VideosCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VideosCategories");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "VideosCategories");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "VideosCategories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PodcastsCollections");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PodcastsCollections");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PodcastsCollections");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PodcastsCollections");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PodcastsCollections");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PodcastsCollections");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "PodcastsCollections");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PodcastsCategories");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "PodcastsCategories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PodcastsCategories");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "PodcastsCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PodcastsCategories");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PodcastsCategories");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "PodcastsCategories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LiveStreamsCategories");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "LiveStreamsCategories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "LiveStreamsCategories");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "LiveStreamsCategories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LiveStreamsCategories");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "LiveStreamsCategories");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "LiveStreamsCategories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CategoriesCollections");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "CategoriesCollections");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "CategoriesCollections");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "CategoriesCollections");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CategoriesCollections");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "CategoriesCollections");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "CategoriesCollections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VideosTags",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "VideosTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "VideosTags",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "VideosTags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "VideosTags",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "VideosTags",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "VideosTags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VideosCollections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "VideosCollections",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "VideosCollections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "VideosCollections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "VideosCollections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "VideosCollections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "VideosCollections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "VideosCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "VideosCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "VideosCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "VideosCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "VideosCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "VideosCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "VideosCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PodcastsCollections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PodcastsCollections",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PodcastsCollections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PodcastsCollections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PodcastsCollections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "PodcastsCollections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "PodcastsCollections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PodcastsCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "PodcastsCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PodcastsCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "PodcastsCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PodcastsCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "PodcastsCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "PodcastsCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LiveStreamsCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "LiveStreamsCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "LiveStreamsCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "LiveStreamsCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "LiveStreamsCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "LiveStreamsCategories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "LiveStreamsCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CategoriesCollections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "CategoriesCollections",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "CategoriesCollections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "CategoriesCollections",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CategoriesCollections",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "CategoriesCollections",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "CategoriesCollections",
                type: "datetime2",
                nullable: true);           

        }
    }
}
