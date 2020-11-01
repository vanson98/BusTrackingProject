using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class update7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Points");

            migrationBuilder.DropColumn(
                name: "BusId",
                table: "Points");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Points");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Points");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Points",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "OriginalIndex",
                table: "Points",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceId",
                table: "Points",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Points",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Points",
                table: "Points",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "c8b2eaac-97db-4b5e-8bee-0a1b796ee835");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5e767dd7-ead0-45bf-82c7-74a5384d6b1b", "AQAAAAEAACcQAAAAEE4fkuBvld/dKRJOk/mUN4BGdxYSqwEq+WnDfbKGZ3kmX0o7AhCrLWGyl2ikljBlKA==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f56ccf30-cd4d-4e55-af80-b73105c898c1", "AQAAAAEAACcQAAAAEPeC/Xupm05qK9514G/vtGKDkd1jHdeOsWGnbMNxKqBrtOSwk2PpnZgwXMEydLkV/w==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "32c7041d-99f0-4879-a309-55ef4facdcc0", "AQAAAAEAACcQAAAAEC7EWIb/EWw10SEFm0psxyIHQaGKw6zkrWZNfA8GlZ3f98jcaYIgiOcNMTgSQN+UJw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Points_RouteId",
                table: "Points",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Points_Routes_RouteId",
                table: "Points",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Points_Routes_RouteId",
                table: "Points");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Points",
                table: "Points");

            migrationBuilder.DropIndex(
                name: "IX_Points_RouteId",
                table: "Points");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Points");

            migrationBuilder.DropColumn(
                name: "OriginalIndex",
                table: "Points");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Points");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Points");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Points",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BusId",
                table: "Points",
                type: "int",
                nullable: false,
                defaultValue: -1);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Points",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Points",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "c9fee0a7-bc9d-48f7-b99d-b2b1b5345d66");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a17dd8d3-94c9-4baf-8e39-bdbaffe0a7e3", "AQAAAAEAACcQAAAAEJBPc3vsVXU65Wt5kba01JKCSwlcq97+LB9Goamg/3x5oFHwooxlAuL0QMnLCjHaAw==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a3d5fc09-8060-4d10-8fb5-05f59cf28c9a", "AQAAAAEAACcQAAAAEJ+KdHgd+VEQQ6060Fjam1ZpUOnNsjXYYtnNBeCfXTMtrtjyNfwBw8SIzHGtJZg/bA==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "01532b48-be02-4804-b611-e0e45d203d90", "AQAAAAEAACcQAAAAEMjQviyBSckV9crHuCtHZ2vxqeuGQRWIv+f5SoBLwRk5Br5BBfbnGo6RoSiFuhrXSQ==" });
        }
    }
}
