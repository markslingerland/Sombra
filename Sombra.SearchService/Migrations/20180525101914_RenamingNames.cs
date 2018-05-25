using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.SearchService.Migrations
{
    public partial class RenamingNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Content",
                newName: "CharityName");

            migrationBuilder.AddColumn<string>(
                name: "CharityActionName",
                table: "Content",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharityActionName",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "CharityName",
                table: "Content",
                newName: "Name");
        }
    }
}
