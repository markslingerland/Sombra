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
                name: "Charity",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CharityId = table.Column<string>(nullable: true),
                    NameOwner = table.Column<string>(nullable: true),
                    NameCharity = table.Column<string>(nullable: false),
                    EmailCharity = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: true),
                    KVKNumber = table.Column<int>(nullable: false),
                    IBAN = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charity", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Charity");
        }
    }
}