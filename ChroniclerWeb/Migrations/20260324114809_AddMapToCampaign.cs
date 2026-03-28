using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChroniclerWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddMapToCampaign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MapContentType",
                table: "Campaigns",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapData",
                table: "Campaigns",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapUrl",
                table: "Campaigns",
                type: "TEXT",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapContentType",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "MapData",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "MapUrl",
                table: "Campaigns");
        }
    }
}
