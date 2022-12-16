using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StefansSuperShop.Migrations
{
    public partial class UpdatedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserEmail",
                table: "NewslettersSent",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserEmail",
                table: "NewslettersSent");
        }
    }
}
