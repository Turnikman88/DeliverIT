using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliverIT.Models.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "StreetName" },
                values: new object[,]
                {
                    { 1, 1, "Vasil Levski 14" },
                    { 2, 2, "blv. Iztochen 23" },
                    { 3, 3, "blv. Halic 12" },
                    { 4, 4, "blv. Zeus 12" },
                    { 5, 5, "blv. Romunska Morava 1" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Electronics" },
                    { 2, "Shoes" },
                    { 3, "Clothing" },
                    { 4, "Medical supplies" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AddressId", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 2, null, "gonzales@speedy.net", "Speedy", "Gonzales" },
                    { 3, null, "dormut@dhl.tr", "Dormut", "Baba" },
                    { 4, null, "ontime@fedex.us", "Stafanakis", "Kurierakis" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Preparing" },
                    { 2, "On the way" },
                    { 3, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AddressId", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, 1, "mishkov@misho.com", "Misho", "Mishkov" },
                    { 2, 2, "petio@mvc.net", "Peter", "Petrov" },
                    { 3, 3, "koksal@asd.tr", "Koksal", "Baba" },
                    { 4, 4, "indebt@greece.gov", "Nikolaos", "Tsitsibaris" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AddressId", "Email", "FirstName", "LastName" },
                values: new object[] { 1, 1, "djoro@ekont.com", "Djoro", "Emploev" });

            migrationBuilder.InsertData(
                table: "WareHouses",
                columns: new[] { "Id", "AddressId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Shipments",
                columns: new[] { "Id", "ArrivalDate", "DepartureDate", "DestinationWareHouseId", "OriginWareHouseId", "StatusId" },
                values: new object[] { 1, new DateTime(2021, 10, 11, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 10, 6, 0, 0, 0, 0, DateTimeKind.Local), 2, 1, 1 });

            migrationBuilder.InsertData(
                table: "Shipments",
                columns: new[] { "Id", "ArrivalDate", "DepartureDate", "DestinationWareHouseId", "OriginWareHouseId", "StatusId" },
                values: new object[] { 2, new DateTime(2021, 10, 11, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2021, 10, 6, 0, 0, 0, 0, DateTimeKind.Local), 2, 1, 1 });

            migrationBuilder.InsertData(
                table: "Parcels",
                columns: new[] { "Id", "CategoryId", "CustomerId", "DeliverToAddress", "ShipmentId", "WareHouseId", "Weight" },
                values: new object[] { 1, 1, 1, true, 1, 1, 1234.5599999999999 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Parcels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WareHouses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WareHouses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
