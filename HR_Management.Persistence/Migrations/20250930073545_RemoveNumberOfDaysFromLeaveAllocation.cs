using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HR_Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNumberOfDaysFromLeaveAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDays",
                schema: "dbo",
                table: "LeaveAllocation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 9, 30, 7, 35, 45, 132, DateTimeKind.Utc).AddTicks(5087),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 30, 6, 49, 12, 182, DateTimeKind.Utc).AddTicks(2746));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "dbo",
                table: "LeaveAllocation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "LeaveType",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DefaultDay", "LastModifiedBy", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9697), 26, null, null, "Annual Leave" },
                    { 2, null, new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9704), 7, null, null, "Sick Leave" },
                    { 3, null, new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9706), 0, null, null, "Hourly Leave" },
                    { 4, null, new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9708), 3, null, null, "Marriage & Bereavement Leave" },
                    { 5, null, new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9710), 90, null, null, "Maternity Leave" },
                    { 6, null, new DateTime(2025, 9, 30, 7, 35, 45, 133, DateTimeKind.Utc).AddTicks(9715), 30, null, null, "Unpaid Leave" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "LeaveType",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 9, 30, 6, 49, 12, 182, DateTimeKind.Utc).AddTicks(2746),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 30, 7, 35, 45, 132, DateTimeKind.Utc).AddTicks(5087));

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "dbo",
                table: "LeaveAllocation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDays",
                schema: "dbo",
                table: "LeaveAllocation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 6, 49, 12, 183, DateTimeKind.Utc).AddTicks(3978));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 6, 49, 12, 183, DateTimeKind.Utc).AddTicks(3982));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 6, 49, 12, 183, DateTimeKind.Utc).AddTicks(3983));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 6, 49, 12, 183, DateTimeKind.Utc).AddTicks(4024));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 10, 19, 12, 183, DateTimeKind.Local).AddTicks(3805));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 10, 19, 12, 183, DateTimeKind.Local).AddTicks(3817));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 10, 19, 12, 183, DateTimeKind.Local).AddTicks(3819));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 30, 10, 19, 12, 183, DateTimeKind.Local).AddTicks(3821));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DateCreated" },
                values: new object[] { new DateTime(2025, 9, 30, 10, 19, 12, 183, DateTimeKind.Local).AddTicks(3859), new DateTime(2025, 9, 30, 6, 49, 12, 183, DateTimeKind.Utc).AddTicks(3855) });
        }
    }
}
