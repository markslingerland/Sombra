using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.CharityActionService.Migrations
{
    public partial class Renaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Charitykey",
                table: "CharityActions",
                newName: "CharityKey");

            migrationBuilder.RenameColumn(
                name: "CharityActionkey",
                table: "CharityActions",
                newName: "CharityActionKey");

            migrationBuilder.RenameColumn(
                name: "NameCharity",
                table: "CharityActions",
                newName: "CharityName");

            migrationBuilder.RenameColumn(
                name: "NameAction",
                table: "CharityActions",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Discription",
                table: "CharityActions",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CharityKey",
                table: "CharityActions",
                newName: "Charitykey");

            migrationBuilder.RenameColumn(
                name: "CharityActionKey",
                table: "CharityActions",
                newName: "CharityActionkey");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CharityActions",
                newName: "NameAction");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "CharityActions",
                newName: "Discription");

            migrationBuilder.RenameColumn(
                name: "CharityName",
                table: "CharityActions",
                newName: "NameCharity");
        }
    }
}
