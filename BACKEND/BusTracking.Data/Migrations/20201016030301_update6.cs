using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class update6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifies");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Students",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StopId",
                table: "StudentCheckIns",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "MonitorId",
                table: "StudentCheckIns",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "CheckInType",
                table: "StudentCheckIns",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Content = table.Column<string>(maxLength: 500, nullable: false),
                    TimeSent = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Notification_StudentId",
                table: "Notification",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Students",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StopId",
                table: "StudentCheckIns",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MonitorId",
                table: "StudentCheckIns",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CheckInType",
                table: "StudentCheckIns",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Notifies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TimeSent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifies", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "ea55278d-0727-4fc4-9cb1-d0beb4f39a31");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ace59943-9ce6-4052-aab3-1eb887e1bdd2", "AQAAAAEAACcQAAAAEAE0m8Eoygeyyi9PhAfl83XBXGtpP67Hse/0Vgti/BnO6ds7hoSlOiNByp9ZLulkWw==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bbcd63e9-22e5-4a76-a151-7e72045bca73", "AQAAAAEAACcQAAAAEL4Gy9kcu5cOzO80qvVXj34yRazk6IeSHCzvSwE+VVbYgsmi7xK8yQvY6RAOqDjSfQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cd2f8bfa-020e-4aa8-b92f-1f2cf3dffb69", "AQAAAAEAACcQAAAAEN13aRHIB0NaFGLn0CJgV7GictejKqP8WcCIEWDfhSFbmpUVljBo9sXjb/rQGphdSQ==" });
        }
    }
}
