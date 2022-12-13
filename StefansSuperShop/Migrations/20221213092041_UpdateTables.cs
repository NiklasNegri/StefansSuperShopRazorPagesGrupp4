using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StefansSuperShop.Migrations
{
    public partial class UpdateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territory_Region_RegionID",
                table: "Territory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Region",
                table: "Region");

            migrationBuilder.DropColumn(
                name: "SendDate",
                table: "NewslettersSent");

            migrationBuilder.DropColumn(
                name: "HasBeenSent",
                table: "Newsletters");

            migrationBuilder.RenameTable(
                name: "Region",
                newName: "Regions");

            migrationBuilder.RenameColumn(
                name: "AspNetUserId",
                table: "NewslettersSent",
                newName: "ApplicationUserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "SendDate",
                table: "Newsletters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Territory_Regions_RegionID",
                table: "Territory",
                column: "RegionID",
                principalTable: "Regions",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territory_Regions_RegionID",
                table: "Territory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "SendDate",
                table: "Newsletters");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "NewslettersSent",
                newName: "AspNetUserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "SendDate",
                table: "NewslettersSent",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenSent",
                table: "Newsletters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Region",
                table: "Region",
                column: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Territory_Region_RegionID",
                table: "Territory",
                column: "RegionID",
                principalTable: "Region",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
