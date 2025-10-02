using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddHoursPerDayToLeaveType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 10, 2, 8, 29, 18, 497, DateTimeKind.Utc).AddTicks(4470),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 30, 7, 35, 45, 132, DateTimeKind.Utc).AddTicks(5087));

            migrationBuilder.AddColumn<int>(
                name: "HoursPerDay",
                schema: "dbo",
                table: "LeaveType",
                type: "int",
                nullable: false,
                defaultValue: 8);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 10, 2, 8, 29, 18, 498, DateTimeKind.Utc).AddTicks(6605));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 10, 2, 8, 29, 18, 498, DateTimeKind.Utc).AddTicks(6608));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 10, 2, 8, 29, 18, 498, DateTimeKind.Utc).AddTicks(6609));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 10, 2, 8, 29, 18, 498, DateTimeKind.Utc).AddTicks(6610));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 10, 2, 8, 29, 18, 498, DateTimeKind.Utc).AddTicks(6540));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 10, 2, 8, 29, 18, 498, DateTimeKind.Utc).AddTicks(6543));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 10, 2, 8, 29, 18, 498, DateTimeKind.Utc).AddTicks(6544));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 10, 2, 8, 29, 18, 498, DateTimeKind.Utc).AddTicks(6545));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2025, 10, 2, 8, 29, 18, 498, DateTimeKind.Utc).AddTicks(6546));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2025, 10, 2, 8, 29, 18, 498, DateTimeKind.Utc).AddTicks(6548));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 10, 2, 11, 59, 18, 498, DateTimeKind.Local).AddTicks(6382));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 10, 2, 11, 59, 18, 498, DateTimeKind.Local).AddTicks(6395));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 10, 2, 11, 59, 18, 498, DateTimeKind.Local).AddTicks(6397));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 10, 2, 11, 59, 18, 498, DateTimeKind.Local).AddTicks(6398));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DateCreated" },
                values: new object[] { new DateTime(2025, 10, 2, 11, 59, 18, 498, DateTimeKind.Local).AddTicks(6439), new DateTime(2025, 10, 2, 8, 29, 18, 498, DateTimeKind.Utc).AddTicks(6436) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoursPerDay",
                schema: "dbo",
                table: "LeaveType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 9, 30, 7, 35, 45, 132, DateTimeKind.Utc).AddTicks(5087),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 10, 2, 8, 29, 18, 497, DateTimeKind.Utc).AddTicks(4470));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9756));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9762));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9764));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9765));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9697));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9704));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9706));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9708));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 5,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9710));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 6,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9715));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 11, 5, 45, 133, DateTimeKind.Local).AddTicks(9427));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 11, 5, 45, 133, DateTimeKind.Local).AddTicks(9446));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 11, 5, 45, 133, DateTimeKind.Local).AddTicks(9448));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 11, 5, 45, 133, DateTimeKind.Local).AddTicks(9450));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DateCreated" },
                values: new object[] { new DateTime(2025, 9, 30, 11, 5, 45, 133, DateTimeKind.Local).AddTicks(9555), new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9550) });
        }
    }
}
