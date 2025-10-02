using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToLeaveRequest : Migration
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
                defaultValue: new DateTime(2025, 9, 14, 10, 35, 37, 823, DateTimeKind.Utc).AddTicks(2344),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 11, 5, 24, 31, 954, DateTimeKind.Utc).AddTicks(5676));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "dbo",
                table: "LeaveRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequest_UserId",
                schema: "dbo",
                table: "LeaveRequest",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequest_User_UserId",
                schema: "dbo",
                table: "LeaveRequest",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequest_User_UserId",
                schema: "dbo",
                table: "LeaveRequest");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequest_UserId",
                schema: "dbo",
                table: "LeaveRequest");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "LeaveRequest");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 9, 11, 5, 24, 31, 954, DateTimeKind.Utc).AddTicks(5676),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 14, 10, 35, 37, 823, DateTimeKind.Utc).AddTicks(2344));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: null);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: null);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: null);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "LeaveStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: null);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 9, 11, 8, 54, 31, 955, DateTimeKind.Local).AddTicks(7109));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 11, 8, 54, 31, 955, DateTimeKind.Local).AddTicks(7137));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2025, 9, 11, 8, 54, 31, 955, DateTimeKind.Local).AddTicks(7138));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 4,
                column: "DateCreated",
                value: new DateTime(2025, 9, 11, 8, 54, 31, 955, DateTimeKind.Local).AddTicks(7140));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DateCreated" },
                values: new object[] { new DateTime(2025, 9, 11, 8, 54, 31, 955, DateTimeKind.Local).AddTicks(7182), null });
        }
    }
}
