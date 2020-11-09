using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Stops_StopId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StopId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StopId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StopDropId",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StopPickId",
                table: "Students",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "e8d00cf9-ffb7-4505-a851-00fed6e1f7ce");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "78efd40a-fa06-4233-8cdc-74e2fd507584", "AQAAAAEAACcQAAAAEDGbjLqfHsiHno4d2WKdkwOaF5WD23K+USQxjAKLtD9wpfvB3Cr5fKlKbgtHfuI+Cg==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "28d88385-a25f-4ac2-b9da-2b701189e57b", "AQAAAAEAACcQAAAAEDVomQlADaY+HsXTnfx4acigE+Zo+cIlFyx8jiBNWphk9VBnmUc9Fo5GNZms3SdUPA==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e59a12f0-b4d0-40ea-bc18-9a97219b03ab", "AQAAAAEAACcQAAAAEJOm8vC7qGRZndnBKSq0F1CbZh1ezpXqNKuW10aMHgpqAuO8NCmZ2ubTJe0sX3u3hw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_StopDropId",
                table: "Students",
                column: "StopDropId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Stops_StopDropId",
                table: "Students",
                column: "StopDropId",
                principalTable: "Stops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Stops_StopDropId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StopDropId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StopDropId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StopPickId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StopId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "7afdfd35-34cf-4224-b6ba-387eda0b5d66");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "eff27e62-89d8-4870-b3ee-eab3ff716311", "AQAAAAEAACcQAAAAEJBRoVp0AaP4SbeXfq7FUZKcqZGcHAkMQeHbN3x5xvSVocGalRiqAbB3x4qnHURltQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "80e42a3f-190b-4a06-9627-e91def86d575", "AQAAAAEAACcQAAAAEEdPPhVUVTS1Ph8WiL3lMBaXdD+2+cPrIe+4opAcqlO+Ekc+NksBUQgc0ByGlah78g==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bc41ec71-f670-416d-8a0a-51aedfa81bf8", "AQAAAAEAACcQAAAAEI34Yd8QNJwgmI2kC0wsHFLgw+PF5QkwqHot9rz8KYgFKzo1OpRNjVqD/FtouOQ3RQ==" });

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
