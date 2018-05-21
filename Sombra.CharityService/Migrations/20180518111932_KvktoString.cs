using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.CharityService.Migrations
{
    public partial class KvktoString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KVKNumber",
                table: "Charities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Charities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Charities");

            migrationBuilder.AlterColumn<int>(
                name: "KVKNumber",
                table: "Charities",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
