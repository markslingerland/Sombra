using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sombra.DonateService.Migrations
{
    public partial class FixTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChartyActionDonations_CharityActions_CharityActionId",
                table: "ChartyActionDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_ChartyActionDonations_Users_UserId",
                table: "ChartyActionDonations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChartyActionDonations",
                table: "ChartyActionDonations");

            migrationBuilder.RenameTable(
                name: "ChartyActionDonations",
                newName: "CharityActionDonations");

            migrationBuilder.RenameIndex(
                name: "IX_ChartyActionDonations_UserId",
                table: "CharityActionDonations",
                newName: "IX_CharityActionDonations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChartyActionDonations_CharityActionId",
                table: "CharityActionDonations",
                newName: "IX_CharityActionDonations_CharityActionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharityActionDonations",
                table: "CharityActionDonations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharityActionDonations_CharityActions_CharityActionId",
                table: "CharityActionDonations",
                column: "CharityActionId",
                principalTable: "CharityActions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharityActionDonations_Users_UserId",
                table: "CharityActionDonations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharityActionDonations_CharityActions_CharityActionId",
                table: "CharityActionDonations");

            migrationBuilder.DropForeignKey(
                name: "FK_CharityActionDonations_Users_UserId",
                table: "CharityActionDonations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharityActionDonations",
                table: "CharityActionDonations");

            migrationBuilder.RenameTable(
                name: "CharityActionDonations",
                newName: "ChartyActionDonations");

            migrationBuilder.RenameIndex(
                name: "IX_CharityActionDonations_UserId",
                table: "ChartyActionDonations",
                newName: "IX_ChartyActionDonations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CharityActionDonations_CharityActionId",
                table: "ChartyActionDonations",
                newName: "IX_ChartyActionDonations_CharityActionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChartyActionDonations",
                table: "ChartyActionDonations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartyActionDonations_CharityActions_CharityActionId",
                table: "ChartyActionDonations",
                column: "CharityActionId",
                principalTable: "CharityActions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChartyActionDonations_Users_UserId",
                table: "ChartyActionDonations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
