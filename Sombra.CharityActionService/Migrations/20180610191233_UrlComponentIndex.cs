using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.CharityActionService.Migrations
{
    public partial class UrlComponentIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UrlComponent",
                table: "CharityActions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CharityActions_UrlComponent",
                table: "CharityActions",
                column: "UrlComponent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CharityActions_UrlComponent",
                table: "CharityActions");

            migrationBuilder.AlterColumn<string>(
                name: "UrlComponent",
                table: "CharityActions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
