using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.CharityService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Charities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CharityKey = table.Column<Guid>(nullable: false),
                    OwnerUserKey = table.Column<Guid>(nullable: false),
                    OwnerUserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    KVKNumber = table.Column<int>(nullable: false),
                    IBAN = table.Column<string>(nullable: true),
                    CoverImage = table.Column<string>(nullable: true),
                    Slogan = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Charities");
        }
    }
}
