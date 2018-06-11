using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.CharityActionService.Migrations
{
    public partial class RefactorCharity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CharityName",
                table: "CharityActions",
                newName: "UrlComponent");

            migrationBuilder.RenameColumn(
                name: "CharityKey",
                table: "CharityActions",
                newName: "CharityId");

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
                name: "IX_CharityActions_CharityId",
                table: "CharityActions",
                column: "CharityId");

            migrationBuilder.CreateIndex(
                name: "IX_Charities_CharityKey",
                table: "Charities",
                column: "CharityKey");

            migrationBuilder.CreateIndex(
                name: "IX_Charities_Url",
                table: "Charities",
                column: "Url");

            migrationBuilder.AddForeignKey(
                name: "FK_CharityActions_Charities_CharityId",
                table: "CharityActions",
                column: "CharityId",
                principalTable: "Charities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharityActions_Charities_CharityId",
                table: "CharityActions");

            migrationBuilder.DropTable(
                name: "Charities");

            migrationBuilder.DropIndex(
                name: "IX_CharityActions_CharityId",
                table: "CharityActions");

            migrationBuilder.RenameColumn(
                name: "UrlComponent",
                table: "CharityActions",
                newName: "CharityName");

            migrationBuilder.RenameColumn(
                name: "CharityId",
                table: "CharityActions",
                newName: "CharityKey");
        }
    }
}
