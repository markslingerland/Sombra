using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.SearchService.Migrations
{
    public partial class AddedCharityActionKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                table: "Content",
                newName: "CharityKey");

            migrationBuilder.AddColumn<Guid>(
                name: "CharityActionKey",
                table: "Content",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharityActionKey",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "CharityKey",
                table: "Content",
                newName: "Key");
        }
    }
}
