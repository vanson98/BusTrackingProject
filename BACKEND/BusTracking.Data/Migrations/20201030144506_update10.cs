using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class update10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCheckIns_Stops_StopId",
                table: "StudentCheckIns");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "StudentCheckIns",
                type: "decimal(10,8)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "StudentCheckIns",
                type: "decimal(11,8)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "27ba294f-56ad-435a-aee5-9c8ef2245957");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9eaf840f-1e61-42f1-9eb0-05f1c82bed86", "AQAAAAEAACcQAAAAEEWZzOLhR6SP73sl0XUGFw5lTsJaYJAeW/EIdGSJm3wjIv6G81MZKnELji26VpYYqw==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "960f80f5-10bd-4ab2-be0b-befcde587b49", "AQAAAAEAACcQAAAAEC0E9yaPAvvUVVyLp0nBIYHmbJ+48HgVstr7z+gvHhIKSloB/Mw92J2PSmExk9VQyA==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b0ae80b0-5418-453f-b07c-1ba145845e13", "AQAAAAEAACcQAAAAENLMdI10NnBSmHdiPoyo1ldzRaypmewb2YN/yV8TTI4d/A+hfkrOH4jmC2+ipaG8fA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCheckIns_Stops_StopId",
                table: "StudentCheckIns",
                column: "StopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCheckIns_Stops_StopId",
                table: "StudentCheckIns");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "StudentCheckIns");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "StudentCheckIns");

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

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCheckIns_Stops_StopId",
                table: "StudentCheckIns",
                column: "StopId",
                principalTable: "Stops",
                principalColumn: "Id");
        }
    }
}
