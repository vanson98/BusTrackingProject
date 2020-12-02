using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneTeacher",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TeacherName",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Students",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "Students",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "WarningCheckIn",
                table: "Students",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "4b9e5fec-d6ac-453c-abbf-b43cea0b4ddb");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TypeAccount" },
                values: new object[] { "56c0f801-76ef-40e7-8357-08ba4e931462", "AQAAAAEAACcQAAAAEO6VQ2oXc6RqAEkSXRX6ShrS096krg+DgZKsfcetmbotK5dP0Uk7Io1kzC84ZAHRtg==", 3 });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4dc355ba-ce79-4f79-bfe0-22dea4544a15", "AQAAAAEAACcQAAAAEJfMH8BNAKJFjJTZSheigUeTTazrh2wTkt0PzVqQPkOZ7RxiDgDM3ePhlkqV125XVQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a3d82327-6e34-437e-b2bd-605b8cfbcbcd", "AQAAAAEAACcQAAAAEE7k2KzsQ/3lEZm078orT2c4H/kxku1m+W/8Ox4UghU1WqikvHQdfNRcrYyFpgCzpA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "WarningCheckIn",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "PhoneTeacher",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "06ab15bb-8d47-43a9-8e34-4c28c2b597fd");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TypeAccount" },
                values: new object[] { "aa574945-35e7-42e1-8623-7e6a4318ac08", "AQAAAAEAACcQAAAAELLSvGxBoMDzVfqpIzeHtKGfhSXV3g+olP+yWTWvEDolTYABvbg3Ke2jBYDtL/VL3g==", 2 });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "91cbacc1-1306-470f-986f-1760716cf81b", "AQAAAAEAACcQAAAAEH2T7OOQrhChJoPV2E5qnEUBbjHrzTqMoGkKDOLf1S2CunPqMHnDgPioA3zHg+m2lA==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ddf2171c-e775-41a2-a311-7e3bfbdd96f5", "AQAAAAEAACcQAAAAEHMAWhTBWx2dZSGju5o1/4r/zIdCMN1UTSctxkiu9I0kcPk+StKd3JZBwuTlgk+DOw==" });
        }
    }
}
