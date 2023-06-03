using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class changeusecase23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "RoleUseCases",
                columns: table => new
                {
                    RoleUseCaseId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUseCases", x => new { x.RoleId, x.RoleUseCaseId });
                    table.ForeignKey(
                        name: "FK_RoleUseCases_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 23, 23, 24, 45, 131, DateTimeKind.Local).AddTicks(3952));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 23, 23, 24, 45, 131, DateTimeKind.Local).AddTicks(4002));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 23, 23, 24, 45, 131, DateTimeKind.Local).AddTicks(4005));

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {;

            migrationBuilder.DropTable(
                name: "RoleUseCases");

          

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 23, 23, 7, 13, 355, DateTimeKind.Local).AddTicks(447));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 23, 23, 7, 13, 355, DateTimeKind.Local).AddTicks(489));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateOfBirth",
                value: new DateTime(2023, 5, 23, 23, 7, 13, 355, DateTimeKind.Local).AddTicks(493));

            migrationBuilder.AddForeignKey(
                name: "FK_UseCase_Role_RoleId",
                table: "UseCase",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
