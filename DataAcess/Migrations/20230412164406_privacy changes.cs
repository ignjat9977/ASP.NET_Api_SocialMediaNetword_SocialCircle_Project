using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class privacychanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Privacy_PrivacyId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Privacy_PrivacyId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_PrivacyId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_PrivacyId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "PrivacyId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "PrivacyId",
                table: "Photos");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 12, 18, 44, 6, 445, DateTimeKind.Local).AddTicks(8707));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 12, 18, 44, 6, 445, DateTimeKind.Local).AddTicks(8753));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 12, 18, 44, 6, 445, DateTimeKind.Local).AddTicks(8757));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrivacyId",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrivacyId",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 12, 18, 16, 18, 171, DateTimeKind.Local).AddTicks(401));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 12, 18, 16, 18, 171, DateTimeKind.Local).AddTicks(445));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateOfBirth",
                value: new DateTime(2023, 4, 12, 18, 16, 18, 171, DateTimeKind.Local).AddTicks(449));

            migrationBuilder.CreateIndex(
                name: "IX_Videos_PrivacyId",
                table: "Videos",
                column: "PrivacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PrivacyId",
                table: "Photos",
                column: "PrivacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Privacy_PrivacyId",
                table: "Photos",
                column: "PrivacyId",
                principalTable: "Privacy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Privacy_PrivacyId",
                table: "Videos",
                column: "PrivacyId",
                principalTable: "Privacy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
