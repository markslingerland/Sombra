using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Sombra.IdentityService.Migrations
{
    public partial class Version2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "CredentialTypes");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Roles",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Permissions",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "CredentialTypes",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 64);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Roles",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Permissions",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 64);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Permissions",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CredentialTypes",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 64);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "CredentialTypes",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }
    }
}
