using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLastLoginToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                schema: "dbo",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 9, 25, 6, 34, 15, 242, DateTimeKind.Utc).AddTicks(6735),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 14, 10, 35, 37, 823, DateTimeKind.Utc).AddTicks(2344));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 10, 4, 15, 244, DateTimeKind.Local).AddTicks(3162));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 10, 4, 15, 244, DateTimeKind.Local).AddTicks(3165));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 10, 4, 15, 244, DateTimeKind.Local).AddTicks(3167));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 10, 4, 15, 244, DateTimeKind.Local).AddTicks(3168));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 10, 4, 15, 244, DateTimeKind.Local).AddTicks(2959));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 10, 4, 15, 244, DateTimeKind.Local).AddTicks(2962));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 10, 4, 15, 244, DateTimeKind.Local).AddTicks(2964));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 25, 10, 4, 15, 244, DateTimeKind.Local).AddTicks(2975));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DateCreated", "LastLogin" },
                values: new object[] { new DateTime(2025, 9, 25, 10, 4, 15, 244, DateTimeKind.Local).AddTicks(3029), new DateTime(2025, 9, 25, 10, 4, 15, 244, DateTimeKind.Local).AddTicks(3020), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLogin",
                schema: "dbo",
                table: "User");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 9, 14, 10, 35, 37, 823, DateTimeKind.Utc).AddTicks(2344),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 25, 6, 34, 15, 242, DateTimeKind.Utc).AddTicks(6735));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 14, 14, 5, 37, 824, DateTimeKind.Local).AddTicks(2724));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 14, 14, 5, 37, 824, DateTimeKind.Local).AddTicks(2728));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 14, 14, 5, 37, 824, DateTimeKind.Local).AddTicks(2729));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 14, 14, 5, 37, 824, DateTimeKind.Local).AddTicks(2730));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 14, 14, 5, 37, 824, DateTimeKind.Local).AddTicks(2548));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 14, 14, 5, 37, 824, DateTimeKind.Local).AddTicks(2550));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 14, 14, 5, 37, 824, DateTimeKind.Local).AddTicks(2552));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 14, 14, 5, 37, 824, DateTimeKind.Local).AddTicks(2554));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DateCreated" },
                values: new object[] { new DateTime(2025, 9, 14, 14, 5, 37, 824, DateTimeKind.Local).AddTicks(2597), new DateTime(2025, 9, 14, 14, 5, 37, 824, DateTimeKind.Local).AddTicks(2593) });
        }
    }
}
