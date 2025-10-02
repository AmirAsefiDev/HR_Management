using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "dbo",
                table: "User",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DateCreated", "Email", "FullName", "IsActive", "LastModifiedBy", "LastModifiedDate", "Mobile", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2025, 8, 24, 20, 30, 17, 993, DateTimeKind.Local).AddTicks(3870), null, null, null, "Amir Asefi", true, null, null, "9123456789", "AQAAAAEAACcQAAAAELaQ+ZNURszTKJSgtjKad7FZmubE351261A/odOknUzd5AumnBboK0TUvdl2R7SXiA==", "Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_User_Mobile",
                schema: "dbo",
                table: "User",
                column: "Mobile",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Mobile",
                schema: "dbo",
                table: "User");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "User");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                schema: "dbo",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
