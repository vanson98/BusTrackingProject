using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Rounds_RoundId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "RouteRounds");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropIndex(
                name: "IX_Students_RoundId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FisrtName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RoundId",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Students",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Stops",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeDropOff",
                table: "Routes",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimePickUp",
                table: "Routes",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Buses",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.UpdateData(
                table: "Buses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Xe 01");

            migrationBuilder.UpdateData(
                table: "Buses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Xe 02");

            migrationBuilder.UpdateData(
                table: "Buses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Xe 03");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "TimeDropOff",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "TimePickUp",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Buses");

            migrationBuilder.AddColumn<string>(
                name: "FisrtName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoundId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RouteRounds",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    RoundId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteRounds", x => new { x.RouteId, x.RoundId });
                    table.ForeignKey(
                        name: "FK_RouteRounds_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteRounds_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "ab3e47d8-3d62-4af7-86a8-e9ad9414bb9c");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6d56e74b-9b07-49cd-a3c7-896b8193e86d", "AQAAAAEAACcQAAAAEPkpngstwvnNeWU8iTGAJt8ZrUjbyGjCPjKlINnRuTrIdbAZrPD6k9IEQTIi0FGnMA==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b9099a79-4be8-49f3-bc7e-c9e7eda2dd96", "AQAAAAEAACcQAAAAEC2w4dxN8ZIyLT0IzJjBnSeAtLcxv7WWUb2efrhdVjNraTnl8pBcGyGCamE2ZKPh2g==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d45ab998-1625-4db8-994f-2246938e7871", "AQAAAAEAACcQAAAAEM8kCrO9L8guTrCxAWHtsEi6XxCD1BkGoxfX3wjRnBNcbUGeaaBW9cfqoY9zGwp9CA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_RoundId",
                table: "Students",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteRounds_RoundId",
                table: "RouteRounds",
                column: "RoundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Rounds_RoundId",
                table: "Students",
                column: "RoundId",
                principalTable: "Rounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
