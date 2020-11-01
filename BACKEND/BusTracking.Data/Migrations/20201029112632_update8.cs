using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class update8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeStop",
                table: "Stops",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "fd54680b-eaae-4dc1-b4fe-53abf91fe3a0");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8ac216f1-8bc6-43b9-b732-c505eaa8f0b2", "AQAAAAEAACcQAAAAEP9o/O4BmVZj/gvXzVvoPoqqJhy7EWluhzvCz0uXO9pOWLLkYpLTLtdIEjJlS4PfwQ==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "152eeb0d-919d-45d3-837d-24e692e6b229", "AQAAAAEAACcQAAAAEFhemKtjEMg0Pm99G1+nZYy5rYq7d9zVRDaiM5+CsKUhZz3jIDiXjvn20ZC8r9ch9w==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a9aee39f-0765-47c0-a0a2-9b8b845a1b96", "AQAAAAEAACcQAAAAEEa7Li1ncjBmGYhDIqLC8wTnm0QSbG8TaIKVFgr055z90TExPZMnPuT5c6UnzvM6zQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeStop",
                table: "Stops");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "c8b2eaac-97db-4b5e-8bee-0a1b796ee835");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5e767dd7-ead0-45bf-82c7-74a5384d6b1b", "AQAAAAEAACcQAAAAEE4fkuBvld/dKRJOk/mUN4BGdxYSqwEq+WnDfbKGZ3kmX0o7AhCrLWGyl2ikljBlKA==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f56ccf30-cd4d-4e55-af80-b73105c898c1", "AQAAAAEAACcQAAAAEPeC/Xupm05qK9514G/vtGKDkd1jHdeOsWGnbMNxKqBrtOSwk2PpnZgwXMEydLkV/w==" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "32c7041d-99f0-4879-a309-55ef4facdcc0", "AQAAAAEAACcQAAAAEC7EWIb/EWw10SEFm0psxyIHQaGKw6zkrWZNfA8GlZ3f98jcaYIgiOcNMTgSQN+UJw==" });
        }
    }
}
