using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.DonateService.Migrations
{
    public partial class ChangedImagetoCoverImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "CharityActions",
                newName: "CoverImage");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Charities",
                newName: "CoverImage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CoverImage",
                table: "CharityActions",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "CoverImage",
                table: "Charities",
                newName: "Image");
        }
    }
}
