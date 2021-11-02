using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliverIT.Models.Migrations
{
    public partial class testnovember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalDate", "DepartureDate" },
                values: new object[] { new DateTime(2021, 11, 12, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 11, 7, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalDate", "DepartureDate" },
                values: new object[] { new DateTime(2021, 11, 12, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 11, 7, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "AppRole");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppRole");

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalDate", "DepartureDate" },
                values: new object[] { new DateTime(2021, 11, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 10, 30, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalDate", "DepartureDate" },
                values: new object[] { new DateTime(2021, 11, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 10, 30, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
