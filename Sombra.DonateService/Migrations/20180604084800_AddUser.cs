using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.DonateService.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Charities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CharityKey = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserKey = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    ProfileImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharityActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CharityActionKey = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ActionEndDateTime = table.Column<DateTime>(nullable: false),
                    CharityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharityActions_Charities_CharityId",
                        column: x => x.CharityId,
                        principalTable: "Charities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharityDonations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateTimeStamp = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    DonationType = table.Column<int>(nullable: false),
                    IsAnonymous = table.Column<bool>(nullable: false),
                    CharityId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharityDonations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharityDonations_Charities_CharityId",
                        column: x => x.CharityId,
                        principalTable: "Charities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharityDonations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChartyActionDonations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateTimeStamp = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    DonationType = table.Column<int>(nullable: false),
                    IsAnonymous = table.Column<bool>(nullable: false),
                    CharityActionId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartyActionDonations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChartyActionDonations_CharityActions_CharityActionId",
                        column: x => x.CharityActionId,
                        principalTable: "CharityActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChartyActionDonations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharityActions_CharityId",
                table: "CharityActions",
                column: "CharityId");

            migrationBuilder.CreateIndex(
                name: "IX_CharityDonations_CharityId",
                table: "CharityDonations",
                column: "CharityId");

            migrationBuilder.CreateIndex(
                name: "IX_CharityDonations_UserId",
                table: "CharityDonations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartyActionDonations_CharityActionId",
                table: "ChartyActionDonations",
                column: "CharityActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartyActionDonations_UserId",
                table: "ChartyActionDonations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharityDonations");

            migrationBuilder.DropTable(
                name: "ChartyActionDonations");

            migrationBuilder.DropTable(
                name: "CharityActions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Charities");
        }
    }
}
