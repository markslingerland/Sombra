using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.DonateService.Migrations
{
    public partial class AddedThankYou : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "CharityActions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThankYou",
                table: "CharityActions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Charities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThankYou",
                table: "Charities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "CharityActions");

            migrationBuilder.DropColumn(
                name: "ThankYou",
                table: "CharityActions");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Charities");

            migrationBuilder.DropColumn(
                name: "ThankYou",
                table: "Charities");
        }
    }
}
