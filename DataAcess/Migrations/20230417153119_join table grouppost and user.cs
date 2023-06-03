using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class jointablegrouppostanduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GroupPost",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 17, 17, 31, 19, 74, DateTimeKind.Local).AddTicks(9356));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 17, 17, 31, 19, 74, DateTimeKind.Local).AddTicks(9416));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 17, 17, 31, 19, 74, DateTimeKind.Local).AddTicks(9424));

            migrationBuilder.CreateIndex(
                name: "IX_GroupPost_UserId",
                table: "GroupPost",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPost_Users_UserId",
                table: "GroupPost",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPost_Users_UserId",
                table: "GroupPost");

            migrationBuilder.DropIndex(
                name: "IX_GroupPost_UserId",
                table: "GroupPost");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GroupPost");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 17, 17, 8, 18, 619, DateTimeKind.Local).AddTicks(3050));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 17, 17, 8, 18, 619, DateTimeKind.Local).AddTicks(3091));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 17, 17, 8, 18, 619, DateTimeKind.Local).AddTicks(3098));
        }
    }
}
