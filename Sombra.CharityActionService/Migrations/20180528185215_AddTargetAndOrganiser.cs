using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.CharityActionService.Migrations
{
    public partial class AddTargetAndOrganiser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActionEndDateTime",
                table: "CharityActions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "CollectedAmount",
                table: "CharityActions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "OrganiserImage",
                table: "CharityActions",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganiserUserKey",
                table: "CharityActions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "OrganiserUserName",
                table: "CharityActions",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TargetAmount",
                table: "CharityActions",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionEndDateTime",
                table: "CharityActions");

            migrationBuilder.DropColumn(
                name: "CollectedAmount",
                table: "CharityActions");

            migrationBuilder.DropColumn(
                name: "OrganiserImage",
                table: "CharityActions");

            migrationBuilder.DropColumn(
                name: "OrganiserUserKey",
                table: "CharityActions");

            migrationBuilder.DropColumn(
                name: "OrganiserUserName",
                table: "CharityActions");

            migrationBuilder.DropColumn(
                name: "TargetAmount",
                table: "CharityActions");
        }
    }
}
