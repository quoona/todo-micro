using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rabbit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "Todos");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCompleted",
                table: "Todos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Todos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 14, 15, 19, 37, 425, DateTimeKind.Local).AddTicks(9342),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 14, 14, 3, 59, 620, DateTimeKind.Local).AddTicks(3997));

            migrationBuilder.AddColumn<Guid>(
                name: "GuidId",
                table: "Todos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("46409e92-56b8-4135-9f39-f0379f6b3723"),
                collation: "ascii_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuidId",
                table: "Todos");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCompleted",
                table: "Todos",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Todos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 14, 14, 3, 59, 620, DateTimeKind.Local).AddTicks(3997),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2025, 4, 14, 15, 19, 37, 425, DateTimeKind.Local).AddTicks(9342));

            migrationBuilder.AddColumn<Guid>(
                name: "GuildId",
                table: "Todos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("0c8492fc-d8e5-463c-b628-64d400822617"),
                collation: "ascii_general_ci");
        }
    }
}
