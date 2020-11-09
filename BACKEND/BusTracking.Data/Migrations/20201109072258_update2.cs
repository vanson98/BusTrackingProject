using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CheckInState",
                table: "StudentCheckIns",
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
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aa574945-35e7-42e1-8623-7e6a4318ac08", "AQAAAAEAACcQAAAAELLSvGxBoMDzVfqpIzeHtKGfhSXV3g+olP+yWTWvEDolTYABvbg3Ke2jBYDtL/VL3g==" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInState",
                table: "StudentCheckIns");

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
        }
    }
}
