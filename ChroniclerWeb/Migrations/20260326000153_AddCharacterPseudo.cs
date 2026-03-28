using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChroniclerWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterPseudo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pseudo",
                table: "Characters",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pseudo",
                table: "Characters");
        }
    }
}
