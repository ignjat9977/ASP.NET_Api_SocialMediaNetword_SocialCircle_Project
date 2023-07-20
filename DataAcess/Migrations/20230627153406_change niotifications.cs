using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class changeniotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "ReciverId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2023, 6, 27, 17, 34, 5, 487, DateTimeKind.Local).AddTicks(4906));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2023, 6, 27, 17, 34, 5, 487, DateTimeKind.Local).AddTicks(4959));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateOfBirth",
                value: new DateTime(2023, 6, 27, 17, 34, 5, 487, DateTimeKind.Local).AddTicks(4967));

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ReciverId",
                table: "Notifications",
                column: "ReciverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_ReciverId",
                table: "Notifications",
                column: "ReciverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_ReciverId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ReciverId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ReciverId",
                table: "Notifications");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2023, 6, 1, 20, 51, 53, 945, DateTimeKind.Local).AddTicks(1421));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2023, 6, 1, 20, 51, 53, 945, DateTimeKind.Local).AddTicks(1492));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateOfBirth",
                value: new DateTime(2023, 6, 1, 20, 51, 53, 945, DateTimeKind.Local).AddTicks(1497));
        }
    }
}
