using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MakeMobileNullableToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Mobile",
                schema: "dbo",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                schema: "dbo",
                table: "User",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "dbo",
                table: "User",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 9, 25, 16, 59, 26, 0, DateTimeKind.Utc).AddTicks(8639),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 25, 13, 37, 47, 193, DateTimeKind.Utc).AddTicks(460));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 20, 29, 26, 2, DateTimeKind.Local).AddTicks(1765));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 20, 29, 26, 2, DateTimeKind.Local).AddTicks(1768));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 20, 29, 26, 2, DateTimeKind.Local).AddTicks(1770));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 20, 29, 26, 2, DateTimeKind.Local).AddTicks(1771));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 20, 29, 26, 2, DateTimeKind.Local).AddTicks(1554));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 20, 29, 26, 2, DateTimeKind.Local).AddTicks(1558));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 20, 29, 26, 2, DateTimeKind.Local).AddTicks(1560));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 20, 29, 26, 2, DateTimeKind.Local).AddTicks(1562));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CountryCode", "CreatedAt", "DateCreated", "Email" },
                values: new object[] { 98, new DateTime(2025, 9, 25, 20, 29, 26, 2, DateTimeKind.Local).AddTicks(1612), new DateTime(2025, 9, 25, 20, 29, 26, 2, DateTimeKind.Local).AddTicks(1607), "amirasefi.info@gmail.com" });

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "dbo",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Mobile",
                schema: "dbo",
                table: "User",
                column: "Mobile",
                unique: true,
                filter: "[Mobile] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Mobile",
                schema: "dbo",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                schema: "dbo",
                table: "User",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "dbo",
                table: "User",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 9, 25, 13, 37, 47, 193, DateTimeKind.Utc).AddTicks(460),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 25, 16, 59, 26, 0, DateTimeKind.Utc).AddTicks(8639));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 17, 7, 47, 194, DateTimeKind.Local).AddTicks(1474));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 17, 7, 47, 194, DateTimeKind.Local).AddTicks(1477));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 17, 7, 47, 194, DateTimeKind.Local).AddTicks(1478));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 17, 7, 47, 194, DateTimeKind.Local).AddTicks(1480));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 17, 7, 47, 194, DateTimeKind.Local).AddTicks(1309));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 17, 7, 47, 194, DateTimeKind.Local).AddTicks(1313));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 17, 7, 47, 194, DateTimeKind.Local).AddTicks(1314));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 17, 7, 47, 194, DateTimeKind.Local).AddTicks(1316));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CountryCode", "CreatedAt", "DateCreated", "Email" },
                values: new object[] { 0, new DateTime(2025, 9, 25, 17, 7, 47, 194, DateTimeKind.Local).AddTicks(1355), new DateTime(2025, 9, 25, 17, 7, 47, 194, DateTimeKind.Local).AddTicks(1352), null });

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "dbo",
                table: "User",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_Mobile",
                schema: "dbo",
                table: "User",
                column: "Mobile",
                unique: true);
        }
    }
}
