using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliverIT.Models.Migrations
{
    public partial class testnovember1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "AppRole");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppRole");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "AppUserRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppUserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "AppUserRoles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppUserRoles");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "AppRole",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppRole",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
