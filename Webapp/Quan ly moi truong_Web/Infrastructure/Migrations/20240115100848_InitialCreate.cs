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
                name: "GarbageDumps",
                columns: table => new
                {
                    GarbageDumpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GarbageDumpName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StreetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WardsWardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_GarbageDumps_Wards_WardsWardId",
                        column: x => x.WardsWardId,
                        principalTable: "Wards",
                        principalColumn: "WardId");
                });

            migrationBuilder.CreateTable(
                name: "ScheduleCleanSidewalks",
                columns: table => new
                {
                    ScheduleCleanSidewalksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_ScheduleCleanSidewalks_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "StreetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleTreeTrims",
                columns: table => new
                {
                    ScheduleTreeTrimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_ScheduleTreeTrims_Streets_StreetId",
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
                name: "ListSidewalkCleanerTasks",
                columns: table => new
                {
                    ListSidewalkCleanerTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleCleanSidewalkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListSidewalkCleanerTasks", x => x.ListSidewalkCleanerTaskId);
                    table.ForeignKey(
                        name: "FK_ListSidewalkCleanerTasks_ScheduleCleanSidewalks_ScheduleCleanSidewalkId",
                        column: x => x.ScheduleCleanSidewalkId,
                        principalTable: "ScheduleCleanSidewalks",
                        principalColumn: "ScheduleCleanSidewalksId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListSidewalkCleanerTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListTreeTrimmerTasks",
                columns: table => new
                {
                    ListTreeTrimmerTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleTreeTrimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListTreeTrimmerTasks", x => x.ListTreeTrimmerTaskId);
                    table.ForeignKey(
                        name: "FK_ListTreeTrimmerTasks_ScheduleTreeTrims_ScheduleTreeTrimId",
                        column: x => x.ScheduleTreeTrimId,
                        principalTable: "ScheduleTreeTrims",
                        principalColumn: "ScheduleTreeTrimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListTreeTrimmerTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
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
                    StreetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetsStreetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduleGarbageCollects_Streets_StreetsStreetId",
                        column: x => x.StreetsStreetId,
                        principalTable: "Streets",
                        principalColumn: "StreetId");
                });

            migrationBuilder.CreateTable(
                name: "ListGarbagemanTasks",
                columns: table => new
                {
                    ListGarbagemanTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleGarbageCollectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListGarbagemanTasks", x => x.ListGarbagemanTaskId);
                    table.ForeignKey(
                        name: "FK_ListGarbagemanTasks_ScheduleGarbageCollects_ScheduleGarbageCollectId",
                        column: x => x.ScheduleGarbageCollectId,
                        principalTable: "ScheduleGarbageCollects",
                        principalColumn: "ScheduleGarbageCollectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListGarbagemanTasks_Users_UserId",
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
                    { new Guid("f9257e9f-6d30-45fd-8afc-3e3266d7adc6"), "123123123Aa", 12f, "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(832), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(833) },
                    { new Guid("f9257e9f-6d31-45fd-8afc-3e3266d7adc6"), "123123123Aa", 12f, "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(846), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(846) }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "CreateBy", "CreateDate", "DepartmentName", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("bc2f24de-1b9b-489a-a108-64a114d2b9be"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(4375), "Cat tia cay", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(4375) },
                    { new Guid("bc2f24de-2b9b-429a-a108-64a114d2b9be"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(4370), "Thu gom rac", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(4371) },
                    { new Guid("bc2f24de-2b9b-489a-a108-64a114d2b9be"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(4356), "Quet don via he", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(4357) }
                });

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "DistrictId", "CreateBy", "CreateDate", "DistrictName", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("be7d62da-33ea-46b0-b294-bb109eca92fc"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(5813), "Thanh Khe", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(5814) },
                    { new Guid("be7d62da-51ea-46b0-b294-bb109eca92fc"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(5818), "Hai Chau", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(5818) },
                    { new Guid("be7d62da-53ea-46b0-b294-bb109eca92fc"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(5802), "Ngu Hanh Son", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(5803) }
                });

            migrationBuilder.InsertData(
                table: "GarbageTruckTypes",
                columns: new[] { "GarbageTruckTypeId", "CreateBy", "CreateDate", "GarbageTruckTypeName", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("12e42a48-f991-4733-bd7c-2e536f921b22"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(1279), "Xe thu gom rac to", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(1279) },
                    { new Guid("12e42a48-f991-4733-bd7c-2e536f931b22"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(1229), "Xe thu gom rac nho", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(1230) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreateBy", "CreateDate", "RoleName", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("8977ef77-e554-4ef3-8353-3e01161f84d0"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(6852), "Employee", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(6853) },
                    { new Guid("abccde85-c7dc-4f78-9e4e-b1b3e7abee84"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(6837), "Manager", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(6838) },
                    { new Guid("cacd4b3a-8afe-43e9-b757-f57f5c61f8d8"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(6848), "Leader", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(6849) }
                });

            migrationBuilder.InsertData(
                table: "StreetTypes",
                columns: new[] { "StreetTypeId", "CreateBy", "CreateDate", "StreetTypeName", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("1be73957-b7e9-4304-9242-00e8b92a86f0"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 394, DateTimeKind.Local).AddTicks(9435), "Duong Kinh Doanh", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 394, DateTimeKind.Local).AddTicks(9436) },
                    { new Guid("e3d44b7e-8ebe-434f-88ef-054a81951be1"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 394, DateTimeKind.Local).AddTicks(9447), "Duong Dan Sinh", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 394, DateTimeKind.Local).AddTicks(9448) }
                });

            migrationBuilder.InsertData(
                table: "TreeTypes",
                columns: new[] { "TreeTypeId", "CreateBy", "CreateDate", "TreeTypeName", "UpdateBy", "UpdateDate" },
                values: new object[] { new Guid("ad98e780-ce3b-401b-a2ec-dd7ba8027642"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 395, DateTimeKind.Local).AddTicks(2388), "Cay than go", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 395, DateTimeKind.Local).AddTicks(2388) });

            migrationBuilder.InsertData(
                table: "Cultivars",
                columns: new[] { "CultivarId", "CreateBy", "CreateDate", "CultivarName", "TreeTypeId", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("136514ac-99a2-221a-80e1-5351d9a9c4af"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(2564), "Giong cay phuong", new Guid("ad98e780-ce3b-401b-a2ec-dd7ba8027642"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(2565) },
                    { new Guid("136514ac-99a2-421a-80e1-5351d9a9c4af"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(2550), "Giong cay bang", new Guid("ad98e780-ce3b-401b-a2ec-dd7ba8027642"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(2550) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "Image", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserCode", "UserName" },
                values: new object[,]
                {
                    { new Guid("56b77536-6c85-4e7d-910b-964e906c7cf2"), 0, "Admin", "c496ef12-197d-4bc4-ac33-881a84abc539", new Guid("bc2f24de-2b9b-489a-a108-64a114d2b9be"), null, false, "string", false, null, "Admin", null, null, "123123Aa!", null, "0947346127", false, new Guid("abccde85-c7dc-4f78-9e4e-b1b3e7abee84"), null, false, "admin", null },
                    { new Guid("b2b1e0ce-0187-4285-8cce-60fdff665f46"), 0, "30 Nam Ky Khoi Nghia", "35f0855f-527c-4b19-b109-27ac61752891", new Guid("bc2f24de-2b9b-489a-a108-64a114d2b9be"), null, false, "string", false, null, "Nguyen Van A", null, null, "123123Aa!", null, "0947123244", false, new Guid("8977ef77-e554-4ef3-8353-3e01161f84d0"), null, false, "NHS_HH_NKKN_123", null },
                    { new Guid("b2b1e0ce-0187-4285-8cce-60fdff666f46"), 0, "45 Huynh Lam", "3c59e210-faa3-474c-8908-85bc62ce7551", new Guid("bc2f24de-2b9b-429a-a108-64a114d2b9be"), null, false, "string", false, null, "Nguyen Van B", null, null, "123123Aa!", null, "0947133244", false, new Guid("8977ef77-e554-4ef3-8353-3e01161f84d0"), null, false, "NHS_HH_NKKN_456", null }
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
                values: new object[] { new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 394, DateTimeKind.Local).AddTicks(7900), 20, 10000f, "Duong Huynh Lam", new Guid("1be73957-b7e9-4304-9242-00e8b92a86f0"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 394, DateTimeKind.Local).AddTicks(7901), new Guid("996c63bc-5f0a-44f6-8c9a-aad741b3beac") });

            migrationBuilder.InsertData(
                table: "GarbageDumps",
                columns: new[] { "GarbageDumpId", "CreateBy", "CreateDate", "GarbageDumpName", "StreetId", "UpdateBy", "UpdateDate", "WardsWardId" },
                values: new object[,]
                {
                    { new Guid("be5d01ee-b15c-4ced-aa0c-165c47dac9f9"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(7663), "HL-HH-NHS_1", new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(7663), null },
                    { new Guid("be5d01ee-b15d-4ced-aa0c-165c47dac9f9"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(7676), "HL-HH-NHS_2", new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(7677), null }
                });

            migrationBuilder.InsertData(
                table: "ScheduleCleanSidewalks",
                columns: new[] { "ScheduleCleanSidewalksId", "CreateBy", "CreateDate", "StartTime", "StreetId", "UpdateBy", "UpdateDate", "WorkingMonth" },
                values: new object[] { new Guid("7a866c85-b013-4fab-80c7-15d21d0c686c"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(7437), new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(7436), new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(7438), new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(7437) });

            migrationBuilder.InsertData(
                table: "ScheduleTreeTrims",
                columns: new[] { "ScheduleTreeTrimId", "ActualTrimmingTime", "BucketTruckId", "CreateBy", "CreateDate", "EstimatedPruningTime", "StreetId", "UpdateBy", "UpdateDate" },
                values: new object[] { new Guid("04dc28f5-94c4-4565-93a2-934d6fee53fd"), new DateTime(2024, 4, 16, 17, 8, 48, 394, DateTimeKind.Local).AddTicks(1077), new Guid("f9257e9f-6d30-45fd-8afc-3e3266d7adc6"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 394, DateTimeKind.Local).AddTicks(1079), new DateTime(2024, 4, 15, 17, 8, 48, 394, DateTimeKind.Local).AddTicks(1076), new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 394, DateTimeKind.Local).AddTicks(1080) });

            migrationBuilder.InsertData(
                table: "Trees",
                columns: new[] { "TreeId", "BodyDiameter", "CreateBy", "CreateDate", "CultivarId", "CutTime", "IntervalCutTime", "LeafLength", "Note", "PlantTime", "StreetId", "TreeCode", "UpdateBy", "UpdateDate" },
                values: new object[] { new Guid("24b2ee45-d7c3-4cc7-9fac-406b4bac1d82"), 30f, "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 395, DateTimeKind.Local).AddTicks(821), new Guid("136514ac-99a2-421a-80e1-5351d9a9c4af"), new DateTime(2024, 4, 15, 17, 8, 48, 395, DateTimeKind.Local).AddTicks(818), 3, 50f, "", new DateTime(2024, 1, 15, 17, 8, 48, 395, DateTimeKind.Local).AddTicks(818), new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "12_HL_HH_NHS", "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 395, DateTimeKind.Local).AddTicks(822) });

            migrationBuilder.InsertData(
                table: "GarbageTrucks",
                columns: new[] { "GarbageTruckId", "CreateBy", "CreateDate", "GarbageDumpId", "GarbageTruckLicensePlates", "GarbageTruckTypeId", "GarbageTruckWeight", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("fc34e805-4550-4037-a273-17a0b1639bbc"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(9702), new Guid("be5d01ee-b15c-4ced-aa0c-165c47dac9f9"), "123456Aa", new Guid("12e42a48-f991-4733-bd7c-2e536f931b22"), 450f, "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(9703) },
                    { new Guid("fc34e805-4550-4037-a273-17a0b1639bbe"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(9688), new Guid("be5d01ee-b15c-4ced-aa0c-165c47dac9f9"), "123123Aa", new Guid("12e42a48-f991-4733-bd7c-2e536f931b22"), 450f, "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 392, DateTimeKind.Local).AddTicks(9690) }
                });

            migrationBuilder.InsertData(
                table: "ListSidewalkCleanerTasks",
                columns: new[] { "ListSidewalkCleanerTaskId", "CreateBy", "CreateDate", "ScheduleCleanSidewalkId", "UpdateBy", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("3c30019e-05f6-4f43-8bd5-3e29ef90031a"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(3207), new Guid("7a866c85-b013-4fab-80c7-15d21d0c686c"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(3207), new Guid("b2b1e0ce-0187-4285-8cce-60fdff666f46") },
                    { new Guid("3c30019e-05f6-4f43-8bd5-3e29ef9004cf"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(3194), new Guid("7a866c85-b013-4fab-80c7-15d21d0c686c"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(3195), new Guid("b2b1e0ce-0187-4285-8cce-60fdff665f46") }
                });

            migrationBuilder.InsertData(
                table: "ListTreeTrimmerTasks",
                columns: new[] { "ListTreeTrimmerTaskId", "CreateBy", "CreateDate", "ScheduleTreeTrimId", "UpdateBy", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("25f83ff6-39d4-461d-82d3-3814cb57fa9c"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(4152), new Guid("04dc28f5-94c4-4565-93a2-934d6fee53fd"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(4154), new Guid("b2b1e0ce-0187-4285-8cce-60fdff665f46") },
                    { new Guid("e13c54c5-1923-49ef-99ab-54a60fed579c"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(4165), new Guid("04dc28f5-94c4-4565-93a2-934d6fee53fd"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(4166), new Guid("b2b1e0ce-0187-4285-8cce-60fdff666f46") }
                });

            migrationBuilder.InsertData(
                table: "ScheduleGarbageCollects",
                columns: new[] { "ScheduleGarbageCollectId", "CreateBy", "CreateDate", "GabageMass", "GarbageTruckId", "StartTime", "StreetId", "StreetsStreetId", "TransitTime", "UpdateBy", "UpdateDate", "WorkingMonth" },
                values: new object[,]
                {
                    { new Guid("26397b2b-ca94-4af4-bf0d-f7aaa7510698"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(9391), 10f, new Guid("fc34e805-4550-4037-a273-17a0b1639bbe"), new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(9386), new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), null, new DateTime(2024, 1, 15, 20, 8, 48, 393, DateTimeKind.Local).AddTicks(9387), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(9391), new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(9390) },
                    { new Guid("e3c19a06-7f84-4c4d-8d83-71264a5cf176"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(9407), 10f, new Guid("fc34e805-4550-4037-a273-17a0b1639bbe"), new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(9405), new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), null, new DateTime(2024, 1, 15, 20, 8, 48, 393, DateTimeKind.Local).AddTicks(9406), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(9407), new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(9406) }
                });

            migrationBuilder.InsertData(
                table: "ListGarbagemanTasks",
                columns: new[] { "ListGarbagemanTaskId", "CreateBy", "CreateDate", "ScheduleGarbageCollectId", "UpdateBy", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("f348026b-3f20-4197-865f-076f47c4cbc7"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(2218), new Guid("26397b2b-ca94-4af4-bf0d-f7aaa7510698"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(2219), new Guid("b2b1e0ce-0187-4285-8cce-60fdff665f46") },
                    { new Guid("f348026b-3f20-4197-865f-076f47c4cbd7"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(2230), new Guid("26397b2b-ca94-4af4-bf0d-f7aaa7510698"), "Admin", new DateTime(2024, 1, 15, 17, 8, 48, 393, DateTimeKind.Local).AddTicks(2231), new Guid("b2b1e0ce-0187-4285-8cce-60fdff666f46") }
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
                name: "IX_GarbageDumps_WardsWardId",
                table: "GarbageDumps",
                column: "WardsWardId");

            migrationBuilder.CreateIndex(
                name: "IX_GarbageTrucks_GarbageDumpId",
                table: "GarbageTrucks",
                column: "GarbageDumpId");

            migrationBuilder.CreateIndex(
                name: "IX_GarbageTrucks_GarbageTruckTypeId",
                table: "GarbageTrucks",
                column: "GarbageTruckTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ListGarbagemanTasks_ScheduleGarbageCollectId",
                table: "ListGarbagemanTasks",
                column: "ScheduleGarbageCollectId");

            migrationBuilder.CreateIndex(
                name: "IX_ListGarbagemanTasks_UserId",
                table: "ListGarbagemanTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListSidewalkCleanerTasks_ScheduleCleanSidewalkId",
                table: "ListSidewalkCleanerTasks",
                column: "ScheduleCleanSidewalkId");

            migrationBuilder.CreateIndex(
                name: "IX_ListSidewalkCleanerTasks_UserId",
                table: "ListSidewalkCleanerTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListTreeTrimmerTasks_ScheduleTreeTrimId",
                table: "ListTreeTrimmerTasks",
                column: "ScheduleTreeTrimId");

            migrationBuilder.CreateIndex(
                name: "IX_ListTreeTrimmerTasks_UserId",
                table: "ListTreeTrimmerTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleCleanSidewalks_StreetId",
                table: "ScheduleCleanSidewalks",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleGarbageCollects_GarbageTruckId",
                table: "ScheduleGarbageCollects",
                column: "GarbageTruckId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleGarbageCollects_StreetsStreetId",
                table: "ScheduleGarbageCollects",
                column: "StreetsStreetId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTreeTrims_BucketTruckId",
                table: "ScheduleTreeTrims",
                column: "BucketTruckId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTreeTrims_StreetId",
                table: "ScheduleTreeTrims",
                column: "StreetId");

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
                name: "ListGarbagemanTasks");

            migrationBuilder.DropTable(
                name: "ListSidewalkCleanerTasks");

            migrationBuilder.DropTable(
                name: "ListTreeTrimmerTasks");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Trees");

            migrationBuilder.DropTable(
                name: "ScheduleGarbageCollects");

            migrationBuilder.DropTable(
                name: "ScheduleCleanSidewalks");

            migrationBuilder.DropTable(
                name: "ScheduleTreeTrims");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cultivars");

            migrationBuilder.DropTable(
                name: "GarbageTrucks");

            migrationBuilder.DropTable(
                name: "Bucket Trucks");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "TreeTypes");

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
