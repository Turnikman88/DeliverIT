using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliverIT.Models.Migrations
{
    public partial class testmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalDate", "DepartureDate" },
                values: new object[] { new DateTime(2021, 10, 13, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 10, 8, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalDate", "DepartureDate" },
                values: new object[] { new DateTime(2021, 10, 13, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 10, 8, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalDate", "DepartureDate" },
                values: new object[] { new DateTime(2021, 10, 11, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 10, 6, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalDate", "DepartureDate" },
                values: new object[] { new DateTime(2021, 10, 11, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 10, 6, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
