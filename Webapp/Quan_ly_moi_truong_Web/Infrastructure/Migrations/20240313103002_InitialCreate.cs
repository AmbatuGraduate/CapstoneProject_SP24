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
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminCreated = table.Column<bool>(type: "bit", nullable: false),
                    DirectMembersCount = table.Column<long>(type: "bigint", nullable: false)
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "ResidentialGroups",
                columns: table => new
                {
                    ResidentialGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResidentialGroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentialGroups", x => x.ResidentialGroupId);
                    table.ForeignKey(
                        name: "FK_ResidentialGroups_Wards_WardId",
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "UserRefreshTokens",
                columns: table => new
                {
                    UserRefreshTokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Expire = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.UserRefreshTokenId);
                    table.ForeignKey(
                        name: "FK_UserRefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
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
                    ResidentialGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.StreetId);
                    table.ForeignKey(
                        name: "FK_Streets_ResidentialGroups_ResidentialGroupId",
                        column: x => x.ResidentialGroupId,
                        principalTable: "ResidentialGroups",
                        principalColumn: "ResidentialGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Streets_StreetTypes_StreetTypeId",
                        column: x => x.StreetTypeId,
                        principalTable: "StreetTypes",
                        principalColumn: "StreetTypeId",
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
                    isCut = table.Column<bool>(type: "bit", nullable: false),
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

            migrationBuilder.InsertData(
                table: "Districts",
                columns: new[] { "DistrictId", "CreateBy", "CreateDate", "DistrictName", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("be7d62da-33ea-46b0-b294-bb109eca92fc"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 722, DateTimeKind.Local).AddTicks(4629), "Thanh Khe", "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 722, DateTimeKind.Local).AddTicks(4630) },
                    { new Guid("be7d62da-51ea-46b0-b294-bb109eca92fc"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 722, DateTimeKind.Local).AddTicks(4634), "Hai Chau", "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 722, DateTimeKind.Local).AddTicks(4634) },
                    { new Guid("be7d62da-53ea-46b0-b294-bb109eca92fc"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 722, DateTimeKind.Local).AddTicks(4615), "Ngu Hanh Son", "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 722, DateTimeKind.Local).AddTicks(4616) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "CreateBy", "CreateDate", "RoleName", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("8977ef77-e554-4ef3-8353-3e01161f84d0"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(140), "Employee", "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(140) },
                    { new Guid("abccde85-c7dc-4f78-9e4e-b1b3e7abee84"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(119), "Manager", "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(121) },
                    { new Guid("cacd4b3a-8afe-43e9-b757-f57f5c61f8d8"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(135), "Leader", "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(136) }
                });

            migrationBuilder.InsertData(
                table: "StreetTypes",
                columns: new[] { "StreetTypeId", "CreateBy", "CreateDate", "StreetTypeName", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("1be73957-b7e9-4304-9242-00e8b92a86f0"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(4455), "Duong Kinh Doanh", "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(4456) },
                    { new Guid("e3d44b7e-8ebe-434f-88ef-054a81951be1"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(4470), "Duong Dan Sinh", "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(4471) }
                });

            migrationBuilder.InsertData(
                table: "TreeTypes",
                columns: new[] { "TreeTypeId", "CreateBy", "CreateDate", "TreeTypeName", "UpdateBy", "UpdateDate" },
                values: new object[] { new Guid("ad98e780-ce3b-401b-a2ec-dd7ba8027642"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(8091), "Cay than go", "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(8092) });

            migrationBuilder.InsertData(
                table: "Cultivars",
                columns: new[] { "CultivarId", "CreateBy", "CreateDate", "CultivarName", "TreeTypeId", "UpdateBy", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("136514ac-99a2-221a-80e1-5351d9a9c4af"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 722, DateTimeKind.Local).AddTicks(627), "Giong cay phuong", new Guid("ad98e780-ce3b-401b-a2ec-dd7ba8027642"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 722, DateTimeKind.Local).AddTicks(627) },
                    { new Guid("136514ac-99a2-421a-80e1-5351d9a9c4af"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 722, DateTimeKind.Local).AddTicks(608), "Giong cay bang", new Guid("ad98e780-ce3b-401b-a2ec-dd7ba8027642"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 722, DateTimeKind.Local).AddTicks(609) }
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
                table: "ResidentialGroups",
                columns: new[] { "ResidentialGroupId", "CreateBy", "CreateDate", "ResidentialGroupName", "UpdateBy", "UpdateDate", "WardId" },
                values: new object[] { new Guid("0a0e931d-d055-48a9-b8a4-2cf57ac2f6f5"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 722, DateTimeKind.Local).AddTicks(8263), "Dong Tra", "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 722, DateTimeKind.Local).AddTicks(8264), new Guid("996c63bc-5f0a-44f6-8c9a-aad741b3beac") });

            migrationBuilder.InsertData(
                table: "Streets",
                columns: new[] { "StreetId", "CreateBy", "CreateDate", "NumberOfHouses", "ResidentialGroupId", "StreetLength", "StreetName", "StreetTypeId", "UpdateBy", "UpdateDate" },
                values: new object[] { new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(2514), 20, new Guid("0a0e931d-d055-48a9-b8a4-2cf57ac2f6f5"), 10000f, "Duong Huynh Lam", new Guid("1be73957-b7e9-4304-9242-00e8b92a86f0"), "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(2515) });

            migrationBuilder.InsertData(
                table: "Trees",
                columns: new[] { "TreeId", "BodyDiameter", "CreateBy", "CreateDate", "CultivarId", "CutTime", "IntervalCutTime", "LeafLength", "Note", "PlantTime", "StreetId", "TreeCode", "UpdateBy", "UpdateDate", "isCut", "isExist" },
                values: new object[] { new Guid("24b2ee45-d7c3-4cc7-9fac-406b4bac1d82"), 30f, "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(6216), new Guid("136514ac-99a2-421a-80e1-5351d9a9c4af"), new DateTime(2024, 6, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(6189), 3, 50f, "", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(6188), new Guid("0c0187dc-c7e2-4aa9-ae35-a5e2d60dfa24"), "12_HL_HH_NHS", "Admin", new DateTime(2024, 3, 13, 17, 30, 2, 723, DateTimeKind.Local).AddTicks(6217), true, true });

            migrationBuilder.CreateIndex(
                name: "IX_Cultivars_TreeTypeId",
                table: "Cultivars",
                column: "TreeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResidentialGroups_WardId",
                table: "ResidentialGroups",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_ResidentialGroupId",
                table: "Streets",
                column: "ResidentialGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_StreetTypeId",
                table: "Streets",
                column: "StreetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trees_CultivarId",
                table: "Trees",
                column: "CultivarId");

            migrationBuilder.CreateIndex(
                name: "IX_Trees_StreetId",
                table: "Trees",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_UserId",
                table: "UserRefreshTokens",
                column: "UserId");

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
                name: "Trees");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "Cultivars");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TreeTypes");

            migrationBuilder.DropTable(
                name: "ResidentialGroups");

            migrationBuilder.DropTable(
                name: "StreetTypes");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "Districts");
        }
    }
}
