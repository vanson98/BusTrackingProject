using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class update9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteStops");

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Stops",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "8780f045-72bc-4b6d-bfe0-6e2224b25f0d");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "65a2f4ca-4acf-45d4-91c9-e886e1f454ae", "AQAAAAEAACcQAAAAEN8mCLfWyQXIPLoNYZFt6Yl8pgzRbpqq1oNqFoiXKXh2Tg5EEIlCm/ztaQdzbdzNMQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "611b6ec7-3508-46b9-8202-35440f64f11e", "AQAAAAEAACcQAAAAEJpBQmBnfNcazB/XrnOQ/QGJKgy+3GOjveFCvcTKuparntlTvVI8DusONaj2twoGYQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "723a4697-c91f-4633-b870-c0f3291f73a9", "AQAAAAEAACcQAAAAECXCILaS2393Yk7MzQro1tPmJ3CmvfJIEr/n4sSpATBNkjc6jnlCSfdE325HXWhL6Q==" });

            migrationBuilder.CreateIndex(
                name: "IX_Stops_RouteId",
                table: "Stops",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Routes_RouteId",
                table: "Stops",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Routes_RouteId",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Stops_RouteId",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Stops");

            migrationBuilder.CreateTable(
                name: "RouteStops",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    StopId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteStops", x => new { x.RouteId, x.StopId });
                    table.ForeignKey(
                        name: "FK_RouteStops_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteStops_Stops_StopId",
                        column: x => x.StopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "fd54680b-eaae-4dc1-b4fe-53abf91fe3a0");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8ac216f1-8bc6-43b9-b732-c505eaa8f0b2", "AQAAAAEAACcQAAAAEP9o/O4BmVZj/gvXzVvoPoqqJhy7EWluhzvCz0uXO9pOWLLkYpLTLtdIEjJlS4PfwQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "152eeb0d-919d-45d3-837d-24e692e6b229", "AQAAAAEAACcQAAAAEFhemKtjEMg0Pm99G1+nZYy5rYq7d9zVRDaiM5+CsKUhZz3jIDiXjvn20ZC8r9ch9w==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a9aee39f-0765-47c0-a0a2-9b8b845a1b96", "AQAAAAEAACcQAAAAEEa7Li1ncjBmGYhDIqLC8wTnm0QSbG8TaIKVFgr055z90TExPZMnPuT5c6UnzvM6zQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_RouteStops_StopId",
                table: "RouteStops",
                column: "StopId");
        }
    }
}
