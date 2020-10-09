using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MonitorId",
                table: "StudentCheckIns",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "58797e1c-e5d8-4209-ba5b-e4dc4a7230aa");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c72b20f6-da9a-4e0b-a701-0f8cf23dc36d", "AQAAAAEAACcQAAAAEJM0syKxYrWedTwLMucjSssCdKzQFPnZPtV/rF3WPhLAXUHRsBANFsbu8/Wuege3jQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1178a7b3-317a-4629-9e05-e30ca7d00f6b", "AQAAAAEAACcQAAAAEM1dzTMKSq1Fw/+6I4Gq/H83QYzwhvtF0SOEo3rWVlwuzFqrXBqEyFSKbR9jHkP/Ag==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d5bf9b5b-22e4-4b9c-82e5-8c5b6d70fb3a", "AQAAAAEAACcQAAAAEL+Lq6QnTq3qTWKNHjA0ZyMWdm6X3Qf0AsWGCpcgQC/m5XZgIXHYEF3e/SLzDPt1HA==" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCheckIns_MonitorId",
                table: "StudentCheckIns",
                column: "MonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCheckIns_StopId",
                table: "StudentCheckIns",
                column: "StopId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCheckIns_StudentId",
                table: "StudentCheckIns",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCheckIns_AppUsers_MonitorId",
                table: "StudentCheckIns",
                column: "MonitorId",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCheckIns_Stops_StopId",
                table: "StudentCheckIns",
                column: "StopId",
                principalTable: "Stops",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCheckIns_Students_StudentId",
                table: "StudentCheckIns",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCheckIns_AppUsers_MonitorId",
                table: "StudentCheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCheckIns_Stops_StopId",
                table: "StudentCheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCheckIns_Students_StudentId",
                table: "StudentCheckIns");

            migrationBuilder.DropIndex(
                name: "IX_StudentCheckIns_MonitorId",
                table: "StudentCheckIns");

            migrationBuilder.DropIndex(
                name: "IX_StudentCheckIns_StopId",
                table: "StudentCheckIns");

            migrationBuilder.DropIndex(
                name: "IX_StudentCheckIns_StudentId",
                table: "StudentCheckIns");

            migrationBuilder.DropColumn(
                name: "MonitorId",
                table: "StudentCheckIns");

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
        }
    }
}
