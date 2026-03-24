using Microsoft.EntityFrameworkCore.Migrations;
using System;

public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AspNetRoles",
            columns: table => new
            {
                Id = table.Column<string>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUsers",
            columns: table => new
            {
                Id = table.Column<string>(type: "TEXT", nullable: false),
                DisplayName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                AvatarUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                AvatarData = table.Column<string>(type: "TEXT", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                PreferredLanguage = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                PlayStyle = table.Column<int>(type: "INTEGER", nullable: true),
                ExperienceLevel = table.Column<int>(type: "INTEGER", nullable: true),
                Bio = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                ProfileCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AspNetRoleClaims",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                RoleId = table.Column<string>(type: "TEXT", nullable: false),
                ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserClaims",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                UserId = table.Column<string>(type: "TEXT", nullable: false),
                ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserLogins",
            columns: table => new
            {
                LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                UserId = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                table.ForeignKey(
                    name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserRoles",
            columns: table => new
            {
                UserId = table.Column<string>(type: "TEXT", nullable: false),
                RoleId = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserTokens",
            columns: table => new
            {
                UserId = table.Column<string>(type: "TEXT", nullable: false),
                LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                Value = table.Column<string>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Campaigns",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                GameSystem = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                LogoUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                LogoData = table.Column<string>(type: "TEXT", nullable: true),
                Status = table.Column<int>(type: "INTEGER", nullable: false),
                RecruitmentStatus = table.Column<int>(type: "INTEGER", nullable: false),
                RecruitmentDescription = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                MaxPlayers = table.Column<int>(type: "INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                OwnerId = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Campaigns", x => x.Id);
                table.ForeignKey(
                    name: "FK_Campaigns_AspNetUsers_OwnerId",
                    column: x => x.OwnerId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "CampaignMembers",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                CampaignId = table.Column<int>(type: "INTEGER", nullable: false),
                UserId = table.Column<string>(type: "TEXT", nullable: false),
                Role = table.Column<int>(type: "INTEGER", nullable: false),
                CanAddNotes = table.Column<bool>(type: "INTEGER", nullable: false),
                JoinedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CampaignMembers", x => x.Id);
                table.ForeignKey(
                    name: "FK_CampaignMembers_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_CampaignMembers_Campaigns_CampaignId",
                    column: x => x.CampaignId,
                    principalTable: "Campaigns",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Characters",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                Role = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                Race = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Class = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                PortraitUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                PortraitData = table.Column<string>(type: "TEXT", nullable: true),
                Status = table.Column<int>(type: "INTEGER", nullable: false),
                Type = table.Column<int>(type: "INTEGER", nullable: false),
                NPCRole = table.Column<int>(type: "INTEGER", nullable: false),
                Strength = table.Column<int>(type: "INTEGER", nullable: false),
                Dexterity = table.Column<int>(type: "INTEGER", nullable: false),
                Constitution = table.Column<int>(type: "INTEGER", nullable: false),
                Intelligence = table.Column<int>(type: "INTEGER", nullable: false),
                Wisdom = table.Column<int>(type: "INTEGER", nullable: false),
                Charisma = table.Column<int>(type: "INTEGER", nullable: false),
                GMNotes = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                SharedNotes = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                CampaignId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Characters", x => x.Id);
                table.ForeignKey(
                    name: "FK_Characters_Campaigns_CampaignId",
                    column: x => x.CampaignId,
                    principalTable: "Campaigns",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

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
            name: "Locations",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                Region = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                Type = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                MapImageUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                MapImageData = table.Column<string>(type: "TEXT", nullable: true),
                MapX = table.Column<double>(type: "REAL", nullable: true),
                MapY = table.Column<double>(type: "REAL", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                CampaignId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Locations", x => x.Id);
                table.ForeignKey(
                    name: "FK_Locations_Campaigns_CampaignId",
                    column: x => x.CampaignId,
                    principalTable: "Campaigns",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Sessions",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                SessionNumber = table.Column<int>(type: "INTEGER", nullable: false),
                SessionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                Notes = table.Column<string>(type: "TEXT", nullable: false),
                Summary = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                CampaignId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Sessions", x => x.Id);
                table.ForeignKey(
                    name: "FK_Sessions_Campaigns_CampaignId",
                    column: x => x.CampaignId,
                    principalTable: "Campaigns",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
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
            name: "Events",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                IsKeyEvent = table.Column<bool>(type: "INTEGER", nullable: false),
                EventDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                CampaignId = table.Column<int>(type: "INTEGER", nullable: false),
                SessionId = table.Column<int>(type: "INTEGER", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Events", x => x.Id);
                table.ForeignKey(
                    name: "FK_Events_Campaigns_CampaignId",
                    column: x => x.CampaignId,
                    principalTable: "Campaigns",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Events_Sessions_SessionId",
                    column: x => x.SessionId,
                    principalTable: "Sessions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateTable(
            name: "SessionCharacters",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                SessionId = table.Column<int>(type: "INTEGER", nullable: false),
                CharacterId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SessionCharacters", x => x.Id);
                table.ForeignKey(
                    name: "FK_SessionCharacters_Characters_CharacterId",
                    column: x => x.CharacterId,
                    principalTable: "Characters",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_SessionCharacters_Sessions_SessionId",
                    column: x => x.SessionId,
                    principalTable: "Sessions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "SessionLocations",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                SessionId = table.Column<int>(type: "INTEGER", nullable: false),
                LocationId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SessionLocations", x => x.Id);
                table.ForeignKey(
                    name: "FK_SessionLocations_Locations_LocationId",
                    column: x => x.LocationId,
                    principalTable: "Locations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_SessionLocations_Sessions_SessionId",
                    column: x => x.SessionId,
                    principalTable: "Sessions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AspNetRoleClaims_RoleId",
            table: "AspNetRoleClaims",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            table: "AspNetRoles",
            column: "NormalizedName",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserClaims_UserId",
            table: "AspNetUserClaims",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserLogins_UserId",
            table: "AspNetUserLogins",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserRoles_RoleId",
            table: "AspNetUserRoles",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            table: "AspNetUsers",
            column: "NormalizedEmail");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            table: "AspNetUsers",
            column: "NormalizedUserName",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_CampaignMembers_CampaignId_UserId",
            table: "CampaignMembers",
            columns: new[] { "CampaignId", "UserId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_CampaignMembers_UserId",
            table: "CampaignMembers",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Campaigns_OwnerId",
            table: "Campaigns",
            column: "OwnerId");

        migrationBuilder.CreateIndex(
            name: "IX_Campaigns_Status",
            table: "Campaigns",
            column: "Status");

        migrationBuilder.CreateIndex(
            name: "IX_Characters_CampaignId",
            table: "Characters",
            column: "CampaignId");

        migrationBuilder.CreateIndex(
            name: "IX_Events_CampaignId",
            table: "Events",
            column: "CampaignId");

        migrationBuilder.CreateIndex(
            name: "IX_Events_IsKeyEvent",
            table: "Events",
            column: "IsKeyEvent");

        migrationBuilder.CreateIndex(
            name: "IX_Events_SessionId",
            table: "Events",
            column: "SessionId");

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
            name: "IX_Locations_CampaignId",
            table: "Locations",
            column: "CampaignId");

        migrationBuilder.CreateIndex(
            name: "IX_SessionCharacters_CharacterId",
            table: "SessionCharacters",
            column: "CharacterId");

        migrationBuilder.CreateIndex(
            name: "IX_SessionCharacters_SessionId_CharacterId",
            table: "SessionCharacters",
            columns: new[] { "SessionId", "CharacterId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_SessionLocations_LocationId",
            table: "SessionLocations",
            column: "LocationId");

        migrationBuilder.CreateIndex(
            name: "IX_SessionLocations_SessionId_LocationId",
            table: "SessionLocations",
            columns: new[] { "SessionId", "LocationId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Sessions_CampaignId_SessionNumber",
            table: "Sessions",
            columns: new[] { "CampaignId", "SessionNumber" },
            unique: true);

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
            name: "AspNetRoleClaims");

        migrationBuilder.DropTable(
            name: "AspNetUserClaims");

        migrationBuilder.DropTable(
            name: "AspNetUserLogins");

        migrationBuilder.DropTable(
            name: "AspNetUserRoles");

        migrationBuilder.DropTable(
            name: "AspNetUserTokens");

        migrationBuilder.DropTable(
            name: "CampaignMembers");

        migrationBuilder.DropTable(
            name: "Events");

        migrationBuilder.DropTable(
            name: "ForumReplies");

        migrationBuilder.DropTable(
            name: "SessionCharacters");

        migrationBuilder.DropTable(
            name: "SessionLocations");

        migrationBuilder.DropTable(
            name: "UploadedFiles");

        migrationBuilder.DropTable(
            name: "AspNetRoles");

        migrationBuilder.DropTable(
            name: "ForumPosts");

        migrationBuilder.DropTable(
            name: "Sessions");

        migrationBuilder.DropTable(
            name: "Characters");

        migrationBuilder.DropTable(
            name: "Locations");

        migrationBuilder.DropTable(
            name: "Campaigns");

        migrationBuilder.DropTable(
            name: "AspNetUsers");
    }
}