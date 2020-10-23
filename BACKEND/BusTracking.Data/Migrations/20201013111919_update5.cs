using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Stops_StopId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ParentConfirm",
                table: "StudentCheckIns");

            migrationBuilder.AlterColumn<int>(
                name: "StopId",
                table: "Students",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClassOfStudent",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneTeacher",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                table: "Students",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Stops",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Notifies",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AppRoles",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifies",
                table: "Notifies",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Stops_StopId",
                table: "Students",
                column: "StopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Stops_StopId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifies",
                table: "Notifies");

            migrationBuilder.DropColumn(
                name: "ClassOfStudent",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PhoneTeacher",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TeacherName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Notifies");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AppRoles");

            migrationBuilder.AlterColumn<int>(
                name: "StopId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "ParentConfirm",
                table: "StudentCheckIns",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Stops_StopId",
                table: "Students",
                column: "StopId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
