using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.CharityService.Migrations
{
    public partial class AddAnbiAndDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Anbi",
                table: "Charities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Charities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anbi",
                table: "Charities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Charities");
        }
    }
}
