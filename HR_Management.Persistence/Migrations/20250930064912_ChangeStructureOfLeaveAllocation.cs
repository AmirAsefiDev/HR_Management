using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStructureOfLeaveAllocation : Migration
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
                defaultValue: new DateTime(2025, 9, 30, 6, 49, 12, 182, DateTimeKind.Utc).AddTicks(2746),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 26, 8, 16, 17, 867, DateTimeKind.Utc).AddTicks(2809));

            migrationBuilder.AddColumn<int>(
                name: "TotalDays",
                schema: "dbo",
                table: "LeaveAllocation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsedDays",
                schema: "dbo",
                table: "LeaveAllocation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "dbo",
                table: "LeaveAllocation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RemainingDays",
                schema: "dbo",
                table: "LeaveAllocation",
                type: "int",
                nullable: false,
                computedColumnSql: "[TotalDays] - [UsedDays]",
                stored: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocation_UserId",
                schema: "dbo",
                table: "LeaveAllocation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocation_User_UserId",
                schema: "dbo",
                table: "LeaveAllocation",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocation_User_UserId",
                schema: "dbo",
                table: "LeaveAllocation");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAllocation_UserId",
                schema: "dbo",
                table: "LeaveAllocation");

            migrationBuilder.DropColumn(
                name: "RemainingDays",
                schema: "dbo",
                table: "LeaveAllocation");

            migrationBuilder.DropColumn(
                name: "TotalDays",
                schema: "dbo",
                table: "LeaveAllocation");

            migrationBuilder.DropColumn(
                name: "UsedDays",
                schema: "dbo",
                table: "LeaveAllocation");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "LeaveAllocation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 9, 26, 8, 16, 17, 867, DateTimeKind.Utc).AddTicks(2809),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 30, 6, 49, 12, 182, DateTimeKind.Utc).AddTicks(2746));

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
                columns: new[] { "CreatedAt", "DateCreated" },
                values: new object[] { new DateTime(2025, 9, 26, 11, 46, 17, 869, DateTimeKind.Local).AddTicks(8098), new DateTime(2025, 9, 26, 8, 16, 17, 869, DateTimeKind.Utc).AddTicks(8091) });
        }
    }
}
