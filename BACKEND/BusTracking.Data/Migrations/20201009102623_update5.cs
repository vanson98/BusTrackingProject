using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassOfStudent",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeDropOff",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TimePickUp",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TypeTransport",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Stops",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AppRoles",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "27366908-83c8-49cc-a2e1-74f2d287073b");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "feac3e7a-c08b-400a-922c-7f589f500ae6", "AQAAAAEAACcQAAAAEH/VzvuA2/CRzh1MlvYaeghHkFT7RhUrEAmj1Qflzgw64q+7DJdOaFMxvz22SFXYwg==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ee997a56-d46b-4152-9e0f-7d45b5c3ea54", "AQAAAAEAACcQAAAAEFuSrh/CEl8T7NW1JQzzHF6IeC098ncdVr1mtIQlLzrJMxrDk1RfxVA5hnCBD/d9TA==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "53cb6401-eb7f-4c46-bc77-fdcb146c1693", "AQAAAAEAACcQAAAAELkompfWJ33pehnzz/mMNA8eNuZJgcwXUvnCFtPMfLI16lIQWAHt4gXJ9rDiilbJpQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassOfStudent",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TimeDropOff",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TimePickUp",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TypeTransport",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AppRoles");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Stops",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string));

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
        }
    }
}
