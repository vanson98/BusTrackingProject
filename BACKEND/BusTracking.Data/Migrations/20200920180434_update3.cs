using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeDropOff",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "TimePickUp",
                table: "Routes");

            migrationBuilder.AddColumn<int>(
                name: "StopId",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeDropOff",
                table: "Stops",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimePickUp",
                table: "Stops",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "StudentCheckIns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    StopId = table.Column<int>(nullable: false),
                    CheckInType = table.Column<int>(nullable: false),
                    CheckInTime = table.Column<DateTime>(nullable: false),
                    CheckInResult = table.Column<int>(nullable: false),
                    ParentConfirm = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCheckIns", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "16170ffe-b696-46a9-b540-e144154ab24d");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f8a94303-5c82-4534-a086-92e00b65adaf", "AQAAAAEAACcQAAAAECnS3Mb+mAchpnbuLOAP1TLfhTsBM1zWJWXr8V3COvkXg5V5Xoh5qowJN8U97C9/sA==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "91057f12-9220-4f06-a85c-cd865448e138", "AQAAAAEAACcQAAAAEK1i+JCmILE4StULn0IIeIFuDBBom4ofQF9/JpxzhsTDGpSYOd0JZBDhv9QnoXXJnw==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fd60a3ff-6d69-478d-a95f-037659c66c1e", "AQAAAAEAACcQAAAAEB8+674tw7cDSJd/4msKmJgTv9nHQEsA9TlgiR3bJdIykiedetOrLEbV2BTIQP5WPA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_StopId",
                table: "Students",
                column: "StopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Stops_StopId",
                table: "Students",
                column: "StopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Stops_StopId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "StudentCheckIns");

            migrationBuilder.DropIndex(
                name: "IX_Students_StopId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StopId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TimeDropOff",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "TimePickUp",
                table: "Stops");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeDropOff",
                table: "Routes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimePickUp",
                table: "Routes",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "736cbcfa-d67a-4793-ab63-378514084dea");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4299a979-580a-4f88-be9b-86fd2f5325ec", "AQAAAAEAACcQAAAAEOJonMmNDkov4Z5Y08g2ARhznZ5nzguE+tKUkgG+9bEqCs1q/OajQc3CQc3+BbTqiA==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b432f0d1-eb3f-4274-bff8-5d9d4c15138a", "AQAAAAEAACcQAAAAEEJlmMv9wApfVPM7HIlQn7NdMuvOvtenBNv02RPmJCTADfXhnOiJzC1Jgxmrxm2nyQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0c20a976-af7a-4d4c-95b5-e88af4fe9f86", "AQAAAAEAACcQAAAAEFl47s1As0DwKhfKjJZtJa8E4ARDkdQDJvcSD2fx0yRg61zwBzZJZ7Pclg7/b6M2DQ==" });

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "TimeDropOff", "TimePickUp" },
                values: new object[] { new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 7, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "TimeDropOff", "TimePickUp" },
                values: new object[] { new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 7, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "TimeDropOff", "TimePickUp" },
                values: new object[] { new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 7, 0, 0, 0) });
        }
    }
}
