using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Stops");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Stops",
                type: "decimal(11,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Stops",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "RouteStops",
                nullable: false,
                defaultValue: 0);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "RouteStops");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Stops",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Stops",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Stops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "2d7eb432-d096-4187-9dac-6daa177be1db");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3ac9abb3-1bf3-4dcb-aebd-0085bec48490", "AQAAAAEAACcQAAAAEOksMVqPSmb0qXaQzg4yvCl10Bkd9hZYqGBwb1dAJpIi21SiOqU/mFGlaAacFTvKXg==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c2d98425-93c4-4146-a7a1-9298480ec9b1", "AQAAAAEAACcQAAAAEJ01bM8R0+6iNFzu0DXi/+PcgXcKWxJeoDYRWEljd0zjxRsnHzLNyrxlGddQnaVA/w==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ffe4025c-f4de-4851-b1f8-05c76b4271a7", "AQAAAAEAACcQAAAAECRSnLCrMp+E6QKLkRIi8Xo6CdaHE3oNjbr2hc6vfAQDy8fltv4NCNkNhfXRjK1VyA==" });
        }
    }
}
