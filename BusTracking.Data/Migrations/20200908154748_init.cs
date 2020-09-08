using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusTracking.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(maxLength: 255, nullable: false),
                    Dob = table.Column<DateTime>(nullable: false),
                    TypeAccount = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Dob = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    Email = table.Column<string>(unicode: false, nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifies",
                columns: table => new
                {
                    Type = table.Column<int>(nullable: false),
                    Content = table.Column<string>(maxLength: 500, nullable: false),
                    TimeSent = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Longitude = table.Column<decimal>(nullable: false),
                    Latitude = table.Column<decimal>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    BusId = table.Column<int>(nullable: false, defaultValue: -1)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStart = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteCode = table.Column<string>(unicode: false, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Distance = table.Column<decimal>(nullable: false),
                    Desctiption = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 255, nullable: false),
                    NumberOfStudents = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Longitude = table.Column<decimal>(nullable: false),
                    Latitude = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseCode = table.Column<string>(maxLength: 12, nullable: false),
                    MaxSize = table.Column<int>(nullable: false),
                    MaxSpeed = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DriverId = table.Column<int>(nullable: true),
                    MonitorId = table.Column<Guid>(nullable: true),
                    RouteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buses_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Buses_AppUsers_MonitorId",
                        column: x => x.MonitorId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Buses_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RouteRounds",
                columns: table => new
                {
                    RouteId = table.Column<int>(nullable: false),
                    RoundId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "RouteStops",
                columns: table => new
                {
                    RouteId = table.Column<int>(nullable: false),
                    StopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteStops", x => new { x.RouteId, x.StopId });
                    table.ForeignKey(
                        name: "FK_RouteStops_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteStops_Stops_StopId",
                        column: x => x.StopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusId = table.Column<int>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: false),
                    RoundId = table.Column<int>(nullable: false),
                    FisrtName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Dob = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    Email = table.Column<string>(unicode: false, nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_AppUsers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), "ab3e47d8-3d62-4af7-86a8-e9ad9414bb9c", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), new Guid("8d04dce2-969a-435d-bba4-df3f325983dc") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "TypeAccount", "UserName" },
                values: new object[,]
                {
                    { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), 0, "6d56e74b-9b07-49cd-a3c7-896b8193e86d", new DateTime(1998, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "vansonnguyen@gmail.com", true, "Nguyễn Văn Sơn", false, null, "vansonnguyen@gmail.com", "admin", "AQAAAAEAACcQAAAAEPkpngstwvnNeWU8iTGAJt8ZrUjbyGjCPjKlINnRuTrIdbAZrPD6k9IEQTIi0FGnMA==", null, false, "", false, 2, "admin" },
                    { new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"), 0, "d45ab998-1625-4db8-994f-2246938e7871", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "vansonnguyen1@gmail.com", true, "Nguyễn Văn Trung", false, null, "vansonnguyen1@gmail.com", "admin1", "AQAAAAEAACcQAAAAEM8kCrO9L8guTrCxAWHtsEi6XxCD1BkGoxfX3wjRnBNcbUGeaaBW9cfqoY9zGwp9CA==", null, false, "", false, 0, "admin" },
                    { new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"), 0, "b9099a79-4be8-49f3-bc7e-c9e7eda2dd96", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "vansonnguyen2@gmail.com", true, "Nguyễn Văn Lâm", false, null, "vansonnguyen2@gmail.com", "admin2", "AQAAAAEAACcQAAAAEC2w4dxN8ZIyLT0IzJjBnSeAtLcxv7WWUb2efrhdVjNraTnl8pBcGyGCamE2ZKPh2g==", null, false, "", false, 0, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "Address", "Dob", "Email", "Name", "PhoneNumber", "Status" },
                values: new object[,]
                {
                    { 1, "Định công", new DateTime(2020, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "abc@gmail.com", "Nguyễn Sơn", "0364735051", 1 },
                    { 2, "Định công", new DateTime(1998, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "abc@gmail.com", "Nguyễn Hoàng", "0364735052", 1 },
                    { 3, "Định công", new DateTime(1998, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "abc@gmail.com", "Nguyễn Minh Đức", "0364735053", 1 }
                });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "Desctiption", "Distance", "Name", "RouteCode", "Status" },
                values: new object[,]
                {
                    { 1, null, 12.34m, "Tuyến 01", "R001", 1 },
                    { 2, null, 10.34m, "Tuyến 02", "R002", 1 },
                    { 3, null, 77.15m, "Tuyến 03", "R003", 1 }
                });

            migrationBuilder.InsertData(
                table: "Buses",
                columns: new[] { "Id", "Description", "DriverId", "LicenseCode", "MaxSize", "MaxSpeed", "MonitorId", "RouteId", "Status" },
                values: new object[] { 1, null, 1, "29S6-81655", 12, 45m, new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), 1, 1 });

            migrationBuilder.InsertData(
                table: "Buses",
                columns: new[] { "Id", "Description", "DriverId", "LicenseCode", "MaxSize", "MaxSpeed", "MonitorId", "RouteId", "Status" },
                values: new object[] { 2, null, 2, "29S6-81656", 10, 40m, new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"), 2, 1 });

            migrationBuilder.InsertData(
                table: "Buses",
                columns: new[] { "Id", "Description", "DriverId", "LicenseCode", "MaxSize", "MaxSpeed", "MonitorId", "RouteId", "Status" },
                values: new object[] { 3, null, 3, "29S6-81657", 22, 35m, new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"), 3, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Buses_DriverId",
                table: "Buses",
                column: "DriverId",
                unique: true,
                filter: "[DriverId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_MonitorId",
                table: "Buses",
                column: "MonitorId",
                unique: true,
                filter: "[MonitorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_RouteId",
                table: "Buses",
                column: "RouteId",
                unique: true,
                filter: "[RouteId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RouteRounds_RoundId",
                table: "RouteRounds",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteStops_StopId",
                table: "RouteStops",
                column: "StopId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_BusId",
                table: "Students",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ParentId",
                table: "Students",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_RoundId",
                table: "Students",
                column: "RoundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "Notifies");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "RouteRounds");

            migrationBuilder.DropTable(
                name: "RouteStops");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
