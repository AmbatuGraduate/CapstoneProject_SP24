using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixEntitiesTrees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trees_Users_UserId",
                table: "Trees");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Trees",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Trees_UserId",
                table: "Trees",
                newName: "IX_Trees_DepartmentId");

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "Trees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("cacd4b3a-8afe-43e9-b757-f57f5c61f8d8"),
                column: "RoleName",
                value: "Admin");

            migrationBuilder.CreateIndex(
                name: "IX_Trees_UsersId",
                table: "Trees",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trees_Departments_DepartmentId",
                table: "Trees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trees_Users_UsersId",
                table: "Trees",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trees_Departments_DepartmentId",
                table: "Trees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trees_Users_UsersId",
                table: "Trees");

            migrationBuilder.DropIndex(
                name: "IX_Trees_UsersId",
                table: "Trees");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Trees");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Trees",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Trees_DepartmentId",
                table: "Trees",
                newName: "IX_Trees_UserId");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("cacd4b3a-8afe-43e9-b757-f57f5c61f8d8"),
                column: "RoleName",
                value: "Leader");

            migrationBuilder.AddForeignKey(
                name: "FK_Trees_Users_UserId",
                table: "Trees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
