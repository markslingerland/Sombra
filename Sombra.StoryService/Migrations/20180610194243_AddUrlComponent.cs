using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.StoryService.Migrations
{
    public partial class AddUrlComponent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CharityName",
                table: "Stories",
                newName: "UrlComponent");

            migrationBuilder.RenameColumn(
                name: "CharityKey",
                table: "Stories",
                newName: "CharityId");

            migrationBuilder.RenameIndex(
                name: "IX_Stories_CharityKey",
                table: "Stories",
                newName: "IX_Stories_CharityId");

            migrationBuilder.AlterColumn<string>(
                name: "UrlComponent",
                table: "Stories",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Charities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CharityKey = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stories_UrlComponent",
                table: "Stories",
                column: "UrlComponent");

            migrationBuilder.CreateIndex(
                name: "IX_Charities_CharityKey",
                table: "Charities",
                column: "CharityKey");

            migrationBuilder.CreateIndex(
                name: "IX_Charities_Url",
                table: "Charities",
                column: "Url");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Charities_CharityId",
                table: "Stories",
                column: "CharityId",
                principalTable: "Charities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Charities_CharityId",
                table: "Stories");

            migrationBuilder.DropTable(
                name: "Charities");

            migrationBuilder.DropIndex(
                name: "IX_Stories_UrlComponent",
                table: "Stories");

            migrationBuilder.RenameColumn(
                name: "UrlComponent",
                table: "Stories",
                newName: "CharityName");

            migrationBuilder.RenameColumn(
                name: "CharityId",
                table: "Stories",
                newName: "CharityKey");

            migrationBuilder.RenameIndex(
                name: "IX_Stories_CharityId",
                table: "Stories",
                newName: "IX_Stories_CharityKey");

            migrationBuilder.AlterColumn<string>(
                name: "CharityName",
                table: "Stories",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
