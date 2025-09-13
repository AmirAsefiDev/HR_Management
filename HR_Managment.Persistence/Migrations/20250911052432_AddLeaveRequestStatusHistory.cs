using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HR_Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaveRequestStatusHistory : Migration
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
                defaultValue: new DateTime(2025, 9, 11, 5, 24, 31, 954, DateTimeKind.Utc).AddTicks(5676),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 11, 4, 24, 17, 612, DateTimeKind.Utc).AddTicks(5177));

            migrationBuilder.CreateTable(
                name: "LeaveRequestStatusHistory",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveRequestId = table.Column<int>(type: "int", nullable: false),
                    LeaveStatusId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    ChangedBy = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequestStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequestStatusHistory_LeaveRequest_LeaveRequestId",
                        column: x => x.LeaveRequestId,
                        principalSchema: "dbo",
                        principalTable: "LeaveRequest",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveRequestStatusHistory_LeaveStatus_LeaveStatusId",
                        column: x => x.LeaveStatusId,
                        principalSchema: "dbo",
                        principalTable: "LeaveStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveRequestStatusHistory_User_ChangedBy",
                        column: x => x.ChangedBy,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Name" },
                values: new object[] { new DateTime(2025, 9, 11, 8, 54, 31, 955, DateTimeKind.Local).AddTicks(7109), "Employee" });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 11, 8, 54, 31, 955, DateTimeKind.Local).AddTicks(7137));

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Role",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "LastModifiedBy", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { 3, null, new DateTime(2025, 9, 11, 8, 54, 31, 955, DateTimeKind.Local).AddTicks(7138), null, null, "HR" },
                    { 4, null, new DateTime(2025, 9, 11, 8, 54, 31, 955, DateTimeKind.Local).AddTicks(7140), null, null, "Manager" }
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 11, 8, 54, 31, 955, DateTimeKind.Local).AddTicks(7182));

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestStatusHistory_ChangedBy",
                schema: "dbo",
                table: "LeaveRequestStatusHistory",
                column: "ChangedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestStatusHistory_LeaveRequestId",
                schema: "dbo",
                table: "LeaveRequestStatusHistory",
                column: "LeaveRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestStatusHistory_LeaveStatusId",
                schema: "dbo",
                table: "LeaveRequestStatusHistory",
                column: "LeaveStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequestStatusHistory",
                schema: "dbo");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateAt",
                schema: "dbo",
                table: "PasswordResetToken",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 9, 11, 4, 24, 17, 612, DateTimeKind.Utc).AddTicks(5177),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 9, 11, 5, 24, 31, 954, DateTimeKind.Utc).AddTicks(5676));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Name" },
                values: new object[] { new DateTime(2025, 9, 11, 7, 54, 17, 613, DateTimeKind.Local).AddTicks(4923), "User" });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2025, 9, 11, 7, 54, 17, 613, DateTimeKind.Local).AddTicks(4939));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 9, 11, 7, 54, 17, 613, DateTimeKind.Local).AddTicks(4983));
        }
    }
}
