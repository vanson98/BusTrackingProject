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
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    DisplayName = table.Column<string>(nullable: true)
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
                name: "Buses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
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
                name: "Points",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Longitude = table.Column<decimal>(nullable: false),
                    Latitude = table.Column<decimal>(nullable: false),
                    OriginalIndex = table.Column<int>(nullable: true),
                    PlaceId = table.Column<string>(nullable: true),
                    RouteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Points_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(nullable: false),
                    NumberOfStudents = table.Column<int>(nullable: false),
                    TimePickUp = table.Column<TimeSpan>(nullable: false),
                    TimeDropOff = table.Column<TimeSpan>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,8)", nullable: false),
                    TypeStop = table.Column<int>(nullable: false),
                    RouteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stops_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusId = table.Column<int>(nullable: false),
                    StopId = table.Column<int>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: false),
                    TeacherName = table.Column<string>(nullable: true),
                    PhoneTeacher = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Dob = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    Email = table.Column<string>(unicode: false, nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: true),
                    ClassOfStudent = table.Column<string>(nullable: true),
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
                        name: "FK_Students_Stops_StopId",
                        column: x => x.StopId,
                        principalTable: "Stops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "StudentCheckIns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    MonitorId = table.Column<Guid>(nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(11,8)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,8)", nullable: false),
                    CheckInType = table.Column<int>(nullable: true),
                    CheckInTime = table.Column<DateTime>(nullable: false),
                    CheckInResult = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCheckIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCheckIns_AppUsers_MonitorId",
                        column: x => x.MonitorId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentCheckIns_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "DisplayName", "Name", "NormalizedName" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), "7afdfd35-34cf-4224-b6ba-387eda0b5d66", "Administrator role", null, "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"), new Guid("8d04dce2-969a-435d-bba4-df3f325983dc") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "TypeAccount", "UserName" },
                values: new object[,]
                {
                    { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), 0, "eff27e62-89d8-4870-b3ee-eab3ff716311", new DateTime(1998, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "quan@gmail.com", true, "Nguyễn Văn Quân", false, null, "quan@gmail.com", "quan", "AQAAAAEAACcQAAAAEJBRoVp0AaP4SbeXfq7FUZKcqZGcHAkMQeHbN3x5xvSVocGalRiqAbB3x4qnHURltQ==", null, false, "", false, 2, "quan" },
                    { new Guid("da5ac2ab-0346-416a-b640-d5915dad85ed"), 0, "bc41ec71-f670-416d-8a0a-51aedfa81bf8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuan@gmail.com", true, "Nguyễn Văn Tuấn", false, null, "tuan@gmail.com", "tuan", "AQAAAAEAACcQAAAAEI34Yd8QNJwgmI2kC0wsHFLgw+PF5QkwqHot9rz8KYgFKzo1OpRNjVqD/FtouOQ3RQ==", null, false, "", false, 0, "tuan" },
                    { new Guid("d5b139c2-3764-431f-900f-ecc01adf5b91"), 0, "80e42a3f-190b-4a06-9627-e91def86d575", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dung@gmail.com", true, "Nguyễn Việt Dũng", false, null, "dung@gmail.com", "dung", "AQAAAAEAACcQAAAAEEdPPhVUVTS1Ph8WiL3lMBaXdD+2+cPrIe+4opAcqlO+Ekc+NksBUQgc0ByGlah78g==", null, false, "", false, 0, "dung" }
                });

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
                name: "IX_Notification_StudentId",
                table: "Notification",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_RouteId",
                table: "Points",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_RouteId",
                table: "Stops",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCheckIns_MonitorId",
                table: "StudentCheckIns",
                column: "MonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCheckIns_StudentId",
                table: "StudentCheckIns",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_BusId",
                table: "Students",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ParentId",
                table: "Students",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StopId",
                table: "Students",
                column: "StopId");
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
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "StudentCheckIns");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
