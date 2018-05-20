using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.CharityActionService.Migrations
{
    public partial class IntialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharityActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CharityActionKey = table.Column<Guid>(nullable: false),
                    CharityKey = table.Column<Guid>(nullable: false),
                    NameCharity = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    IBAN = table.Column<string>(nullable: true),
                    NameAction = table.Column<string>(nullable: true),
                    ActionType = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CoverImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CharityActionId = table.Column<Guid>(nullable: false),
                    Key = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserKeys_CharityActions_CharityActionId",
                        column: x => x.CharityActionId,
                        principalTable: "CharityActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserKeys_CharityActionId",
                table: "UserKeys",
                column: "CharityActionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserKeys");

            migrationBuilder.DropTable(
                name: "CharityActions");
        }
    }
}
