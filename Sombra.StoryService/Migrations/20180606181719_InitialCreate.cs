using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.StoryService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserKey = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProfileImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StoryKey = table.Column<Guid>(nullable: false),
                    CharityKey = table.Column<Guid>(nullable: false),
                    CharityName = table.Column<string>(nullable: true),
                    AuthorId = table.Column<Guid>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    CoverImage = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    OpeningText = table.Column<string>(nullable: true),
                    StoryImage = table.Column<string>(nullable: true),
                    CoreText = table.Column<string>(nullable: true),
                    QuoteText = table.Column<string>(nullable: true),
                    ConclusionText = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stories_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Base64 = table.Column<string>(nullable: true),
                    StoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_StoryId",
                table: "Images",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_AuthorId",
                table: "Stories",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_CharityKey",
                table: "Stories",
                column: "CharityKey");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_StoryKey",
                table: "Stories",
                column: "StoryKey");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserKey",
                table: "Users",
                column: "UserKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
