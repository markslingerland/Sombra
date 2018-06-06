using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.CharityService.Migrations
{
    public partial class AddIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Charities",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Charities_CharityKey",
                table: "Charities",
                column: "CharityKey");

            migrationBuilder.CreateIndex(
                name: "IX_Charities_Url",
                table: "Charities",
                column: "Url");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Charities_CharityKey",
                table: "Charities");

            migrationBuilder.DropIndex(
                name: "IX_Charities_Url",
                table: "Charities");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Charities",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
