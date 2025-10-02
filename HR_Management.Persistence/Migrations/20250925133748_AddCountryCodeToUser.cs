using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryCodeToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryCode",
                schema: "dbo",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 9, 25, 13, 37, 47, 193, DateTimeKind.Utc).AddTicks(460),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 25, 6, 34, 15, 242, DateTimeKind.Utc).AddTicks(6735));

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
                columns: new[] { "CountryCode", "CreatedAt", "DateCreated" },
                values: new object[] { 0, new DateTime(2025, 9, 25, 17, 7, 47, 194, DateTimeKind.Local).AddTicks(1355), new DateTime(2025, 9, 25, 17, 7, 47, 194, DateTimeKind.Local).AddTicks(1352) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                schema: "dbo",
                table: "User");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 9, 25, 6, 34, 15, 242, DateTimeKind.Utc).AddTicks(6735),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 25, 13, 37, 47, 193, DateTimeKind.Utc).AddTicks(460));

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
                columns: new[] { "CreatedAt", "DateCreated" },
                values: new object[] { new DateTime(2025, 9, 25, 10, 4, 15, 244, DateTimeKind.Local).AddTicks(3029), new DateTime(2025, 9, 25, 10, 4, 15, 244, DateTimeKind.Local).AddTicks(3020) });
        }
    }
}
