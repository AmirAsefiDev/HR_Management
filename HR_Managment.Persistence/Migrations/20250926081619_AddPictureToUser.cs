using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPictureToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                schema: "dbo",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 9, 26, 8, 16, 17, 867, DateTimeKind.Utc).AddTicks(2809),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 25, 16, 59, 26, 0, DateTimeKind.Utc).AddTicks(8639));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 26, 8, 16, 17, 869, DateTimeKind.Utc).AddTicks(8474));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 26, 8, 16, 17, 869, DateTimeKind.Utc).AddTicks(8482));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 26, 8, 16, 17, 869, DateTimeKind.Utc).AddTicks(8486));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 26, 8, 16, 17, 869, DateTimeKind.Utc).AddTicks(8490));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 26, 11, 46, 17, 869, DateTimeKind.Local).AddTicks(7996));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 26, 11, 46, 17, 869, DateTimeKind.Local).AddTicks(8016));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 26, 11, 46, 17, 869, DateTimeKind.Local).AddTicks(8020));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 26, 11, 46, 17, 869, DateTimeKind.Local).AddTicks(8024));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DateCreated", "Picture" },
                values: new object[] { new DateTime(2025, 9, 26, 11, 46, 17, 869, DateTimeKind.Local).AddTicks(8098), new DateTime(2025, 9, 26, 8, 16, 17, 869, DateTimeKind.Utc).AddTicks(8091), "/images/user/default_profile.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                schema: "dbo",
                table: "User");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 9, 25, 16, 59, 26, 0, DateTimeKind.Utc).AddTicks(8639),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 26, 8, 16, 17, 867, DateTimeKind.Utc).AddTicks(2809));

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
                columns: new[] { "CreatedAt", "DateCreated" },
                values: new object[] { new DateTime(2025, 9, 25, 20, 29, 26, 2, DateTimeKind.Local).AddTicks(1612), new DateTime(2025, 9, 25, 20, 29, 26, 2, DateTimeKind.Local).AddTicks(1607) });
        }
    }
}
