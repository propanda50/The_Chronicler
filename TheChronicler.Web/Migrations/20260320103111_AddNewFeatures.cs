using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheChronicler.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MapImageData",
                table: "Locations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapImageUrl",
                table: "Locations",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MapX",
                table: "Locations",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MapY",
                table: "Locations",
                type: "REAL",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Characters",
                type: "TEXT",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<int>(
                name: "Charisma",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Constitution",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dexterity",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GMNotes",
                table: "Characters",
                type: "TEXT",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Intelligence",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NPCRole",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PortraitData",
                table: "Characters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortraitUrl",
                table: "Characters",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SharedNotes",
                table: "Characters",
                type: "TEXT",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Strength",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Wisdom",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LogoData",
                table: "Campaigns",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Campaigns",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxPlayers",
                table: "Campaigns",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RecruitmentDescription",
                table: "Campaigns",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecruitmentStatus",
                table: "Campaigns",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AvatarData",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExperienceLevel",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayStyle",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredLanguage",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ProfileCompleted",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ForumPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 10000, nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CampaignId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForumPosts_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForumPosts_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UploadedFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FileName = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ContentType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FileSize = table.Column<long>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    FileData = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UploadedById = table.Column<string>(type: "TEXT", nullable: false),
                    CampaignId = table.Column<int>(type: "INTEGER", nullable: true),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: true),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_AspNetUsers_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UploadedFiles_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ForumReplies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PostId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuthorId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForumReplies_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForumReplies_ForumPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "ForumPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumPosts_AuthorId",
                table: "ForumPosts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumPosts_CampaignId",
                table: "ForumPosts",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumReplies_AuthorId",
                table: "ForumReplies",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumReplies_PostId",
                table: "ForumReplies",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_CampaignId",
                table: "UploadedFiles",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_CharacterId",
                table: "UploadedFiles",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_LocationId",
                table: "UploadedFiles",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_UploadedById",
                table: "UploadedFiles",
                column: "UploadedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForumReplies");

            migrationBuilder.DropTable(
                name: "UploadedFiles");

            migrationBuilder.DropTable(
                name: "ForumPosts");

            migrationBuilder.DropColumn(
                name: "MapImageData",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "MapImageUrl",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "MapX",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "MapY",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Charisma",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Constitution",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Dexterity",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "GMNotes",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Intelligence",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "NPCRole",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PortraitData",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "PortraitUrl",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "SharedNotes",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Strength",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Wisdom",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "LogoData",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "MaxPlayers",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "RecruitmentDescription",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "RecruitmentStatus",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "AvatarData",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ExperienceLevel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlayStyle",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PreferredLanguage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileCompleted",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Characters",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
