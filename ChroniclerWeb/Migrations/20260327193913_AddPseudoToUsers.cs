using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChroniclerWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddPseudoToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UploadedFiles",
                type: "TEXT",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "UploadedFiles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ForumPostId",
                table: "UploadedFiles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "UploadedFiles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedPseudo",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pseudo",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_EventId",
                table: "UploadedFiles",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_ForumPostId",
                table: "UploadedFiles",
                column: "ForumPostId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadedFiles_SessionId",
                table: "UploadedFiles",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NormalizedPseudo",
                table: "AspNetUsers",
                column: "NormalizedPseudo");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadedFiles_Events_EventId",
                table: "UploadedFiles",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UploadedFiles_ForumPosts_ForumPostId",
                table: "UploadedFiles",
                column: "ForumPostId",
                principalTable: "ForumPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UploadedFiles_Sessions_SessionId",
                table: "UploadedFiles",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadedFiles_Events_EventId",
                table: "UploadedFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UploadedFiles_ForumPosts_ForumPostId",
                table: "UploadedFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UploadedFiles_Sessions_SessionId",
                table: "UploadedFiles");

            migrationBuilder.DropIndex(
                name: "IX_UploadedFiles_EventId",
                table: "UploadedFiles");

            migrationBuilder.DropIndex(
                name: "IX_UploadedFiles_ForumPostId",
                table: "UploadedFiles");

            migrationBuilder.DropIndex(
                name: "IX_UploadedFiles_SessionId",
                table: "UploadedFiles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NormalizedPseudo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "UploadedFiles");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "UploadedFiles");

            migrationBuilder.DropColumn(
                name: "ForumPostId",
                table: "UploadedFiles");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "UploadedFiles");

            migrationBuilder.DropColumn(
                name: "NormalizedPseudo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Pseudo",
                table: "AspNetUsers");
        }
    }
}
