using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheChronicler.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddConstitutionEndurance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Endurance",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endurance",
                table: "Characters");
        }
    }
}
