using Microsoft.EntityFrameworkCore.Migrations;
using System;

public partial class AddPortraitContentTypeToCharacter : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "Agility",
            table: "Characters",
            type: "INTEGER",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<int>(
            name: "Health",
            table: "Characters",
            type: "INTEGER",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<string>(
            name: "PortraitContentType",
            table: "Characters",
            type: "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "AvatarContentType",
            table: "AspNetUsers",
            type: "TEXT",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "Achievements",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                Icon = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                Type = table.Column<int>(type: "INTEGER", nullable: false),
                XP = table.Column<int>(type: "INTEGER", nullable: false),
                Category = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                RequiredCount = table.Column<int>(type: "INTEGER", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Achievements", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "PlayerAchievements",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                PlayerId = table.Column<string>(type: "TEXT", nullable: false),
                AchievementId = table.Column<int>(type: "INTEGER", nullable: false),
                CurrentProgress = table.Column<int>(type: "INTEGER", nullable: false),
                IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                FirstProgressAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PlayerAchievements", x => x.Id);
                table.ForeignKey(
                    name: "FK_PlayerAchievements_Achievements_AchievementId",
                    column: x => x.AchievementId,
                    principalTable: "Achievements",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Achievements_Name",
            table: "Achievements",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_PlayerAchievements_AchievementId",
            table: "PlayerAchievements",
            column: "AchievementId");

        migrationBuilder.CreateIndex(
            name: "IX_PlayerAchievements_PlayerId_AchievementId",
            table: "PlayerAchievements",
            columns: new[] { "PlayerId", "AchievementId" },
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PlayerAchievements");

        migrationBuilder.DropTable(
            name: "Achievements");

        migrationBuilder.DropColumn(
            name: "Agility",
            table: "Characters");

        migrationBuilder.DropColumn(
            name: "Health",
            table: "Characters");

        migrationBuilder.DropColumn(
            name: "PortraitContentType",
            table: "Characters");

        migrationBuilder.DropColumn(
            name: "AvatarContentType",
            table: "AspNetUsers");
    }
}
