using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bucket Trucks",
                columns: table => new
                {
                    BucketTruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BucketTruckLicensePlates = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CraneArmLength = table.Column<float>(type: "real", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bucket Trucks", x => x.BucketTruckId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DistrictName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.DistrictId);
                });

            migrationBuilder.CreateTable(
                name: "GarbageTruckTypes",
                columns: table => new
                {
                    GarbageTruckTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GarbageTruckTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarbageTruckTypes", x => x.GarbageTruckTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleCleanSidewalks",
                columns: table => new
                {
                    ScheduleCleanSidewalksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkingMonth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleCleanSidewalks", x => x.ScheduleCleanSidewalksId);
                });

            migrationBuilder.CreateTable(
                name: "StreetTypes",
                columns: table => new
                {
                    StreetTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetTypes", x => x.StreetTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TreeTypes",
                columns: table => new
                {
                    TreeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreeTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeTypes", x => x.TreeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTreeTrims",
                columns: table => new
                {
                    ScheduleTreeTrimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BucketTruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstimatedPruningTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualTrimmingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTreeTrims", x => x.ScheduleTreeTrimId);
                    table.ForeignKey(
                        name: "FK_ScheduleTreeTrims_Bucket Trucks_BucketTruckId",
                        column: x => x.BucketTruckId,
                        principalTable: "Bucket Trucks",
                        principalColumn: "BucketTruckId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                columns: table => new
                {
                    WardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WardName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DistrictId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.WardId);
                    table.ForeignKey(
                        name: "FK_Wards_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cultivars",
                columns: table => new
                {
                    CultivarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CultivarName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TreeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cultivars", x => x.CultivarId);
                    table.ForeignKey(
                        name: "FK_Cultivars_TreeTypes_TreeTypeId",
                        column: x => x.TreeTypeId,
                        principalTable: "TreeTypes",
                        principalColumn: "TreeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    StreetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StreetLength = table.Column<float>(type: "real", nullable: false),
                    NumberOfHouses = table.Column<int>(type: "int", nullable: false),
                    StreetTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.StreetId);
                    table.ForeignKey(
                        name: "FK_Streets_StreetTypes_StreetTypeId",
                        column: x => x.StreetTypeId,
                        principalTable: "StreetTypes",
                        principalColumn: "StreetTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Streets_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "WardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedbackBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Reports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_scheduleCleanSidewalk_maps",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleCleanSidewalkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_scheduleCleanSidewalk_maps", x => new { x.UserId, x.ScheduleCleanSidewalkId });
                    table.ForeignKey(
                        name: "FK_User_scheduleCleanSidewalk_maps_ScheduleCleanSidewalks_ScheduleCleanSidewalkId",
                        column: x => x.ScheduleCleanSidewalkId,
                        principalTable: "ScheduleCleanSidewalks",
                        principalColumn: "ScheduleCleanSidewalksId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_scheduleCleanSidewalk_maps_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_scheduleTreeTrim_maps",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleTreeTrimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_scheduleTreeTrim_maps", x => new { x.UserId, x.ScheduleTreeTrimId });
                    table.ForeignKey(
                        name: "FK_User_scheduleTreeTrim_maps_ScheduleTreeTrims_ScheduleTreeTrimId",
                        column: x => x.ScheduleTreeTrimId,
                        principalTable: "ScheduleTreeTrims",
                        principalColumn: "ScheduleTreeTrimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_scheduleTreeTrim_maps_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarbageDumps",
                columns: table => new
                {
                    GarbageDumpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GarbageDumpName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StreetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarbageDumps", x => x.GarbageDumpId);
                    table.ForeignKey(
                        name: "FK_GarbageDumps_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "StreetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleCleanSidewalk_street_maps",
                columns: table => new
                {
                    StreetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleCleanSidewalksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleCleanSidewalk_street_maps", x => new { x.StreetId, x.ScheduleCleanSidewalksId });
                    table.ForeignKey(
                        name: "FK_ScheduleCleanSidewalk_street_maps_ScheduleCleanSidewalks_ScheduleCleanSidewalksId",
                        column: x => x.ScheduleCleanSidewalksId,
                        principalTable: "ScheduleCleanSidewalks",
                        principalColumn: "ScheduleCleanSidewalksId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleCleanSidewalk_street_maps_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "StreetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTreeTrim_street_maps",
                columns: table => new
                {
                    StreetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleTreeTrimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTreeTrim_street_maps", x => new { x.StreetId, x.ScheduleTreeTrimId });
                    table.ForeignKey(
                        name: "FK_ScheduleTreeTrim_street_maps_ScheduleTreeTrims_ScheduleTreeTrimId",
                        column: x => x.ScheduleTreeTrimId,
                        principalTable: "ScheduleTreeTrims",
                        principalColumn: "ScheduleTreeTrimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleTreeTrim_street_maps_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "StreetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trees",
                columns: table => new
                {
                    TreeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TreeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StreetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BodyDiameter = table.Column<float>(type: "real", nullable: false),
                    LeafLength = table.Column<float>(type: "real", nullable: false),
                    PlantTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IntervalCutTime = table.Column<int>(type: "int", nullable: false),
                    CultivarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: true),
                    isExist = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trees", x => x.TreeId);
                    table.ForeignKey(
                        name: "FK_Trees_Cultivars_CultivarId",
                        column: x => x.CultivarId,
                        principalTable: "Cultivars",
                        principalColumn: "CultivarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trees_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "StreetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarbageTrucks",
                columns: table => new
                {
                    GarbageTruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GarbageTruckLicensePlates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GarbageTruckWeight = table.Column<float>(type: "real", nullable: false),
                    GarbageTruckTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GarbageDumpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarbageTrucks", x => x.GarbageTruckId);
                    table.ForeignKey(
                        name: "FK_GarbageTrucks_GarbageDumps_GarbageDumpId",
                        column: x => x.GarbageDumpId,
                        principalTable: "GarbageDumps",
                        principalColumn: "GarbageDumpId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GarbageTrucks_GarbageTruckTypes_GarbageTruckTypeId",
                        column: x => x.GarbageTruckTypeId,
                        principalTable: "GarbageTruckTypes",
                        principalColumn: "GarbageTruckTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleGarbageCollects",
                columns: table => new
                {
                    ScheduleGarbageCollectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GabageMass = table.Column<float>(type: "real", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransitTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkingMonth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GarbageTruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleGarbageCollects", x => x.ScheduleGarbageCollectId);
                    table.ForeignKey(
                        name: "FK_ScheduleGarbageCollects_GarbageTrucks_GarbageTruckId",
                        column: x => x.GarbageTruckId,
                        principalTable: "GarbageTrucks",
                        principalColumn: "GarbageTruckId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleGarbageCollect_street_maps",
                columns: table => new
                {
                    StreetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleGarbageCollectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleGarbageCollect_street_maps", x => new { x.StreetId, x.ScheduleGarbageCollectId });
                    table.ForeignKey(
                        name: "FK_ScheduleGarbageCollect_street_maps_ScheduleGarbageCollects_ScheduleGarbageCollectId",
                        column: x => x.ScheduleGarbageCollectId,
                        principalTable: "ScheduleGarbageCollects",
                        principalColumn: "ScheduleGarbageCollectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleGarbageCollect_street_maps_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "StreetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_scheduleGarbageCollect_maps",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleGarbageCollectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_scheduleGarbageCollect_maps", x => new { x.UserId, x.ScheduleGarbageCollectId });
                    table.ForeignKey(
                        name: "FK_User_scheduleGarbageCollect_maps_ScheduleGarbageCollects_ScheduleGarbageCollectId",
                        column: x => x.ScheduleGarbageCollectId,
                        principalTable: "ScheduleGarbageCollects",
                        principalColumn: "ScheduleGarbageCollectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_scheduleGarbageCollect_maps_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bucket Trucks",
                columns: new[] { "BucketTruckId", "BucketTruckLicensePlates", "CraneArmLength", "CreateBy", "CreateDate", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("f9257e9f-6d30-45fd-8afc-3e3266d7adc6"), "123123123Aa", 12f, "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 341, DateTimeKind.Local).AddTicks(3892), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 341, DateTimeKind.Local).AddTicks(3895) },
                    { new Guid("f9257e9f-6d31-45fd-8afc-3e3266d7adc6"), "123123123Aa", 12f, "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 341, DateTimeKind.Local).AddTicks(4001), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 341, DateTimeKind.Local).AddTicks(4003) }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "CreateBy", "CreateDate", "DepartmentName", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("bc2f24de-1b9b-489a-a108-64a114d2b9be"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 343, DateTimeKind.Local).AddTicks(1733), "Cat tia cay", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 343, DateTimeKind.Local).AddTicks(1736) },
                    { new Guid("bc2f24de-2b9b-429a-a108-64a114d2b9be"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 343, DateTimeKind.Local).AddTicks(1695), "Thu gom rac", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 343, DateTimeKind.Local).AddTicks(1699) },
                    { new Guid("bc2f24de-2b9b-489a-a108-64a114d2b9be"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 343, DateTimeKind.Local).AddTicks(1626), "Quet don via he", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 343, DateTimeKind.Local).AddTicks(1630) }
                });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "DistrictId", "CreateBy", "CreateDate", "DistrictName", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("be7d62da-33ea-46b0-b294-bb109eca92fc"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 344, DateTimeKind.Local).AddTicks(115), "Thanh Khe", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 344, DateTimeKind.Local).AddTicks(133) },
                    { new Guid("be7d62da-51ea-46b0-b294-bb109eca92fc"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 344, DateTimeKind.Local).AddTicks(146), "Hai Chau", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 344, DateTimeKind.Local).AddTicks(157) },
                    { new Guid("be7d62da-53ea-46b0-b294-bb109eca92fc"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 344, DateTimeKind.Local).AddTicks(60), "Ngu Hanh Son", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 344, DateTimeKind.Local).AddTicks(63) }
                });

            migrationBuilder.InsertData(
                table: "GarbageTruckTypes",
                columns: new[] { "GarbageTruckTypeId", "CreateBy", "CreateDate", "GarbageTruckTypeName", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("12e42a48-f991-4733-bd7c-2e536f921b22"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 345, DateTimeKind.Local).AddTicks(9614), "Xe thu gom rac to", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 345, DateTimeKind.Local).AddTicks(9617) },
                    { new Guid("12e42a48-f991-4733-bd7c-2e536f931b22"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 345, DateTimeKind.Local).AddTicks(9567), "Xe thu gom rac nho", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 345, DateTimeKind.Local).AddTicks(9570) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreateBy", "CreateDate", "RoleName", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("8977ef77-e554-4ef3-8353-3e01161f84d0"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 347, DateTimeKind.Local).AddTicks(232), "Employee", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 347, DateTimeKind.Local).AddTicks(234) },
                    { new Guid("abccde85-c7dc-4f78-9e4e-b1b3e7abee84"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 347, DateTimeKind.Local).AddTicks(186), "Manager", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 347, DateTimeKind.Local).AddTicks(188) },
                    { new Guid("cacd4b3a-8afe-43e9-b757-f57f5c61f8d8"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 347, DateTimeKind.Local).AddTicks(220), "Leader", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 347, DateTimeKind.Local).AddTicks(222) }
                });

            migrationBuilder.InsertData(
                table: "ScheduleCleanSidewalks",
                columns: new[] { "ScheduleCleanSidewalksId", "CreateBy", "CreateDate", "StartTime", "UpdateBy", "UpdateDate", "WorkingMonth" },
                values: new object[] { new Guid("7a866c85-b013-4fab-80c7-15d21d0c686c"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 347, DateTimeKind.Local).AddTicks(7621), new DateTime(2024, 2, 6, 12, 50, 3, 347, DateTimeKind.Local).AddTicks(7618), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 347, DateTimeKind.Local).AddTicks(7624), new DateTime(2024, 2, 6, 12, 50, 3, 347, DateTimeKind.Local).AddTicks(7620) });

            migrationBuilder.InsertData(
                table: "StreetTypes",
                columns: new[] { "StreetTypeId", "CreateBy", "CreateDate", "StreetTypeName", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("1be73957-b7e9-4304-9242-00e8b92a86f0"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 355, DateTimeKind.Local).AddTicks(8804), "Duong Kinh Doanh", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 355, DateTimeKind.Local).AddTicks(8807) },
                    { new Guid("e3d44b7e-8ebe-434f-88ef-054a81951be1"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 355, DateTimeKind.Local).AddTicks(9098), "Duong Dan Sinh", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 355, DateTimeKind.Local).AddTicks(9100) }
                });

            migrationBuilder.InsertData(
                table: "TreeTypes",
                columns: new[] { "TreeTypeId", "CreateBy", "CreateDate", "TreeTypeName", "UpdateBy", "UpdateDate" },
                values: new object[] { new Guid("ad98e780-ce3b-401b-a2ec-dd7ba8027642"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 357, DateTimeKind.Local).AddTicks(1022), "Cay than go", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 357, DateTimeKind.Local).AddTicks(1024) });

            migrationBuilder.InsertData(
                table: "Cultivars",
                columns: new[] { "CultivarId", "CreateBy", "CreateDate", "CultivarName", "TreeTypeId", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("136514ac-99a2-221a-80e1-5351d9a9c4af"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 342, DateTimeKind.Local).AddTicks(1122), "Giong cay phuong", new Guid("ad98e780-ce3b-401b-a2ec-dd7ba8027642"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 342, DateTimeKind.Local).AddTicks(1124) },
                    { new Guid("136514ac-99a2-421a-80e1-5351d9a9c4af"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 342, DateTimeKind.Local).AddTicks(1082), "Giong cay bang", new Guid("ad98e780-ce3b-401b-a2ec-dd7ba8027642"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 342, DateTimeKind.Local).AddTicks(1085) }
                });

            migrationBuilder.InsertData(
                table: "ScheduleTreeTrims",
                columns: new[] { "ScheduleTreeTrimId", "ActualTrimmingTime", "BucketTruckId", "CreateBy", "CreateDate", "EstimatedPruningTime", "UpdateBy", "UpdateDate" },
                values: new object[] { new Guid("04dc28f5-94c4-4565-93a2-934d6fee53fd"), new DateTime(2024, 5, 7, 12, 50, 3, 350, DateTimeKind.Local).AddTicks(9481), new Guid("f9257e9f-6d30-45fd-8afc-3e3266d7adc6"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 350, DateTimeKind.Local).AddTicks(9489), new DateTime(2024, 5, 6, 12, 50, 3, 350, DateTimeKind.Local).AddTicks(9470), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 350, DateTimeKind.Local).AddTicks(9491) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "Image", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserCode", "UserName" },
                values: new object[,]
                {
                    { new Guid("56b77536-6c85-4e7d-910b-964e906c7cf2"), 0, "Admin", "8de20835-0da0-4f3f-bf2b-11413c6506a4", new Guid("bc2f24de-2b9b-489a-a108-64a114d2b9be"), null, false, "string", false, null, "Admin", null, null, "123123Aa!", null, "0947346127", false, new Guid("abccde85-c7dc-4f78-9e4e-b1b3e7abee84"), null, false, "admin", null },
                    { new Guid("b2b1e0ce-0187-4285-8cce-60fdff665f46"), 0, "30 Nam Ky Khoi Nghia", "c5235e50-aff9-4851-ae69-4bc3cd82d34b", new Guid("bc2f24de-2b9b-489a-a108-64a114d2b9be"), null, false, "string", false, null, "Nguyen Van A", null, null, "123123Aa!", null, "0947123244", false, new Guid("8977ef77-e554-4ef3-8353-3e01161f84d0"), null, false, "NHS_HH_NKKN_123", null },
                    { new Guid("b2b1e0ce-0187-4285-8cce-60fdff666f46"), 0, "45 Huynh Lam", "41bd0194-8a11-4b7e-a3d1-2aa09983aee0", new Guid("bc2f24de-2b9b-429a-a108-64a114d2b9be"), null, false, "string", false, null, "Nguyen Van B", null, null, "123123Aa!", null, "0947133244", false, new Guid("8977ef77-e554-4ef3-8353-3e01161f84d0"), null, false, "NHS_HH_NKKN_456", null }
                });

            migrationBuilder.InsertData(
                table: "Wards",
                columns: new[] { "WardId", "DistrictId", "WardName" },
                values: new object[,]
                {
                    { new Guid("3097108f-15fe-4ac8-aab4-187b56841c81"), new Guid("be7d62da-53ea-46b0-b294-bb109eca92fc"), "Hai Chau II" },
                    { new Guid("38af1dbf-b83f-4899-8389-743021c463a0"), new Guid("be7d62da-53ea-46b0-b294-bb109eca92fc"), "My An" },
                    { new Guid("79bea4b4-23ce-41ad-b585-4dfc835d607a"), new Guid("be7d62da-53ea-46b0-b294-bb109eca92fc"), "Hoa Quy" },
                    { new Guid("996c63bc-5f0a-44f6-8c9a-aad741b3beac"), new Guid("be7d62da-53ea-46b0-b294-bb109eca92fc"), "Hoa Hai" },
                    { new Guid("c088acde-18ea-48ca-ae03-bdd4e610e039"), new Guid("be7d62da-53ea-46b0-b294-bb109eca92fc"), "Hai Chau I" },
                    { new Guid("f4e93702-9dc2-4288-8f23-4c3812ed50cc"), new Guid("be7d62da-53ea-46b0-b294-bb109eca92fc"), "Thuan Phuoc" },
                    { new Guid("faa64719-904b-4844-9ec9-d8f2620ffb51"), new Guid("be7d62da-53ea-46b0-b294-bb109eca92fc"), "Khue My" }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportId", "Content", "Feedback", "FeedbackBy", "Image", "Status", "Title", "UserId" },
                values: new object[] { new Guid("6e4ba4c3-6edf-45ca-8b60-54caa256c725"), "Demo", null, null, "string", false, "DEMO", new Guid("b2b1e0ce-0187-4285-8cce-60fdff665f46") });

            migrationBuilder.InsertData(
                table: "Streets",
                columns: new[] { "StreetId", "CreateBy", "CreateDate", "NumberOfHouses", "StreetLength", "StreetName", "StreetTypeId", "UpdateBy", "UpdateDate", "WardId" },
                values: new object[] { new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 355, DateTimeKind.Local).AddTicks(95), 20, 10000f, "Duong Huynh Lam", new Guid("1be73957-b7e9-4304-9242-00e8b92a86f0"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 355, DateTimeKind.Local).AddTicks(98), new Guid("996c63bc-5f0a-44f6-8c9a-aad741b3beac") });

            migrationBuilder.InsertData(
                table: "User_scheduleCleanSidewalk_maps",
                columns: new[] { "ScheduleCleanSidewalkId", "UserId", "CreateBy", "CreateDate", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("7a866c85-b013-4fab-80c7-15d21d0c686c"), new Guid("b2b1e0ce-0187-4285-8cce-60fdff665f46"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 357, DateTimeKind.Local).AddTicks(7137), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 357, DateTimeKind.Local).AddTicks(7140) },
                    { new Guid("7a866c85-b013-4fab-80c7-15d21d0c686c"), new Guid("b2b1e0ce-0187-4285-8cce-60fdff666f46"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 357, DateTimeKind.Local).AddTicks(7170), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 357, DateTimeKind.Local).AddTicks(7172) }
                });

            migrationBuilder.InsertData(
                table: "User_scheduleTreeTrim_maps",
                columns: new[] { "ScheduleTreeTrimId", "UserId", "CreateBy", "CreateDate", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("04dc28f5-94c4-4565-93a2-934d6fee53fd"), new Guid("b2b1e0ce-0187-4285-8cce-60fdff665f46"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 359, DateTimeKind.Local).AddTicks(9359), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 359, DateTimeKind.Local).AddTicks(9364) },
                    { new Guid("04dc28f5-94c4-4565-93a2-934d6fee53fd"), new Guid("b2b1e0ce-0187-4285-8cce-60fdff666f46"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 359, DateTimeKind.Local).AddTicks(9422), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 359, DateTimeKind.Local).AddTicks(9425) }
                });

            migrationBuilder.InsertData(
                table: "GarbageDumps",
                columns: new[] { "GarbageDumpId", "CreateBy", "CreateDate", "GarbageDumpName", "StreetId", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("be5d01ee-b15c-4ced-aa0c-165c47dac9f9"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 344, DateTimeKind.Local).AddTicks(6941), "HL-HH-NHS_1", new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 344, DateTimeKind.Local).AddTicks(6943) },
                    { new Guid("be5d01ee-b15d-4ced-aa0c-165c47dac9f9"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 344, DateTimeKind.Local).AddTicks(6975), "HL-HH-NHS_2", new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 344, DateTimeKind.Local).AddTicks(6977) }
                });

            migrationBuilder.InsertData(
                table: "ScheduleCleanSidewalk_street_maps",
                columns: new[] { "ScheduleCleanSidewalksId", "StreetId", "CreateBy", "CreateDate", "UpdateBy", "UpdateDate" },
                values: new object[] { new Guid("7a866c85-b013-4fab-80c7-15d21d0c686c"), new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 347, DateTimeKind.Local).AddTicks(5942), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 347, DateTimeKind.Local).AddTicks(5944) });

            migrationBuilder.InsertData(
                table: "ScheduleTreeTrim_street_maps",
                columns: new[] { "ScheduleTreeTrimId", "StreetId", "CreateBy", "CreateDate", "UpdateBy", "UpdateDate" },
                values: new object[] { new Guid("04dc28f5-94c4-4565-93a2-934d6fee53fd"), new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 349, DateTimeKind.Local).AddTicks(5880), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 349, DateTimeKind.Local).AddTicks(5884) });

            migrationBuilder.InsertData(
                table: "Trees",
                columns: new[] { "TreeId", "BodyDiameter", "CreateBy", "CreateDate", "CultivarId", "CutTime", "IntervalCutTime", "LeafLength", "Note", "PlantTime", "StreetId", "TreeCode", "UpdateBy", "UpdateDate", "isExist" },
                values: new object[] { new Guid("24b2ee45-d7c3-4cc7-9fac-406b4bac1d82"), 30f, "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 356, DateTimeKind.Local).AddTicks(4839), new Guid("136514ac-99a2-421a-80e1-5351d9a9c4af"), new DateTime(2024, 5, 6, 12, 50, 3, 356, DateTimeKind.Local).AddTicks(4825), 3, 50f, "", new DateTime(2024, 2, 6, 12, 50, 3, 356, DateTimeKind.Local).AddTicks(4824), new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "12_HL_HH_NHS", "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 356, DateTimeKind.Local).AddTicks(4841), true });

            migrationBuilder.InsertData(
                table: "GarbageTrucks",
                columns: new[] { "GarbageTruckId", "CreateBy", "CreateDate", "GarbageDumpId", "GarbageTruckLicensePlates", "GarbageTruckTypeId", "GarbageTruckWeight", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("fc34e805-4550-4037-a273-17a0b1639bbc"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 345, DateTimeKind.Local).AddTicks(4173), new Guid("be5d01ee-b15c-4ced-aa0c-165c47dac9f9"), "123456Aa", new Guid("12e42a48-f991-4733-bd7c-2e536f931b22"), 450f, "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 345, DateTimeKind.Local).AddTicks(4175) },
                    { new Guid("fc34e805-4550-4037-a273-17a0b1639bbe"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 345, DateTimeKind.Local).AddTicks(4136), new Guid("be5d01ee-b15c-4ced-aa0c-165c47dac9f9"), "123123Aa", new Guid("12e42a48-f991-4733-bd7c-2e536f931b22"), 450f, "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 345, DateTimeKind.Local).AddTicks(4138) }
                });

            migrationBuilder.InsertData(
                table: "ScheduleGarbageCollects",
                columns: new[] { "ScheduleGarbageCollectId", "CreateBy", "CreateDate", "GabageMass", "GarbageTruckId", "StartTime", "TransitTime", "UpdateBy", "UpdateDate", "WorkingMonth" },
                values: new object[,]
                {
                    { new Guid("26397b2b-ca94-4af4-bf0d-f7aaa7510698"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 349, DateTimeKind.Local).AddTicks(249), 10f, new Guid("fc34e805-4550-4037-a273-17a0b1639bbe"), new DateTime(2024, 2, 6, 12, 50, 3, 349, DateTimeKind.Local).AddTicks(239), new DateTime(2024, 2, 6, 15, 50, 3, 349, DateTimeKind.Local).AddTicks(240), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 349, DateTimeKind.Local).AddTicks(251), new DateTime(2024, 2, 6, 12, 50, 3, 349, DateTimeKind.Local).AddTicks(248) },
                    { new Guid("e3c19a06-7f84-4c4d-8d83-71264a5cf176"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 349, DateTimeKind.Local).AddTicks(290), 10f, new Guid("fc34e805-4550-4037-a273-17a0b1639bbe"), new DateTime(2024, 2, 6, 12, 50, 3, 349, DateTimeKind.Local).AddTicks(285), new DateTime(2024, 2, 6, 15, 50, 3, 349, DateTimeKind.Local).AddTicks(286), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 349, DateTimeKind.Local).AddTicks(292), new DateTime(2024, 2, 6, 12, 50, 3, 349, DateTimeKind.Local).AddTicks(288) }
                });

            migrationBuilder.InsertData(
                table: "ScheduleGarbageCollect_street_maps",
                columns: new[] { "ScheduleGarbageCollectId", "StreetId", "CreateBy", "CreateDate", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("26397b2b-ca94-4af4-bf0d-f7aaa7510698"), new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 348, DateTimeKind.Local).AddTicks(3159), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 348, DateTimeKind.Local).AddTicks(3161) },
                    { new Guid("e3c19a06-7f84-4c4d-8d83-71264a5cf176"), new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 348, DateTimeKind.Local).AddTicks(3190), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 348, DateTimeKind.Local).AddTicks(3192) }
                });

            migrationBuilder.InsertData(
                table: "User_scheduleGarbageCollect_maps",
                columns: new[] { "ScheduleGarbageCollectId", "UserId", "CreateBy", "CreateDate", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("26397b2b-ca94-4af4-bf0d-f7aaa7510698"), new Guid("b2b1e0ce-0187-4285-8cce-60fdff665f46"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 358, DateTimeKind.Local).AddTicks(5397), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 358, DateTimeKind.Local).AddTicks(5400) },
                    { new Guid("26397b2b-ca94-4af4-bf0d-f7aaa7510698"), new Guid("b2b1e0ce-0187-4285-8cce-60fdff666f46"), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 358, DateTimeKind.Local).AddTicks(5440), "Admin", new DateTime(2024, 2, 6, 12, 50, 3, 358, DateTimeKind.Local).AddTicks(5442) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cultivars_TreeTypeId",
                table: "Cultivars",
                column: "TreeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GarbageDumps_StreetId",
                table: "GarbageDumps",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_GarbageTrucks_GarbageDumpId",
                table: "GarbageTrucks",
                column: "GarbageDumpId");

            migrationBuilder.CreateIndex(
                name: "IX_GarbageTrucks_GarbageTruckTypeId",
                table: "GarbageTrucks",
                column: "GarbageTruckTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleCleanSidewalk_street_maps_ScheduleCleanSidewalksId",
                table: "ScheduleCleanSidewalk_street_maps",
                column: "ScheduleCleanSidewalksId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleGarbageCollect_street_maps_ScheduleGarbageCollectId",
                table: "ScheduleGarbageCollect_street_maps",
                column: "ScheduleGarbageCollectId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleGarbageCollects_GarbageTruckId",
                table: "ScheduleGarbageCollects",
                column: "GarbageTruckId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTreeTrim_street_maps_ScheduleTreeTrimId",
                table: "ScheduleTreeTrim_street_maps",
                column: "ScheduleTreeTrimId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTreeTrims_BucketTruckId",
                table: "ScheduleTreeTrims",
                column: "BucketTruckId");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_StreetTypeId",
                table: "Streets",
                column: "StreetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_WardId",
                table: "Streets",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Trees_CultivarId",
                table: "Trees",
                column: "CultivarId");

            migrationBuilder.CreateIndex(
                name: "IX_Trees_StreetId",
                table: "Trees",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_User_scheduleCleanSidewalk_maps_ScheduleCleanSidewalkId",
                table: "User_scheduleCleanSidewalk_maps",
                column: "ScheduleCleanSidewalkId");

            migrationBuilder.CreateIndex(
                name: "IX_User_scheduleGarbageCollect_maps_ScheduleGarbageCollectId",
                table: "User_scheduleGarbageCollect_maps",
                column: "ScheduleGarbageCollectId");

            migrationBuilder.CreateIndex(
                name: "IX_User_scheduleTreeTrim_maps_ScheduleTreeTrimId",
                table: "User_scheduleTreeTrim_maps",
                column: "ScheduleTreeTrimId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_DistrictId",
                table: "Wards",
                column: "DistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "ScheduleCleanSidewalk_street_maps");

            migrationBuilder.DropTable(
                name: "ScheduleGarbageCollect_street_maps");

            migrationBuilder.DropTable(
                name: "ScheduleTreeTrim_street_maps");

            migrationBuilder.DropTable(
                name: "Trees");

            migrationBuilder.DropTable(
                name: "User_scheduleCleanSidewalk_maps");

            migrationBuilder.DropTable(
                name: "User_scheduleGarbageCollect_maps");

            migrationBuilder.DropTable(
                name: "User_scheduleTreeTrim_maps");

            migrationBuilder.DropTable(
                name: "Cultivars");

            migrationBuilder.DropTable(
                name: "ScheduleCleanSidewalks");

            migrationBuilder.DropTable(
                name: "ScheduleGarbageCollects");

            migrationBuilder.DropTable(
                name: "ScheduleTreeTrims");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TreeTypes");

            migrationBuilder.DropTable(
                name: "GarbageTrucks");

            migrationBuilder.DropTable(
                name: "Bucket Trucks");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "GarbageDumps");

            migrationBuilder.DropTable(
                name: "GarbageTruckTypes");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropTable(
                name: "StreetTypes");

            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "Districts");
        }
    }
}
