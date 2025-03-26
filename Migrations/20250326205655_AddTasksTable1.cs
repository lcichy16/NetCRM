using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetCRM.Migrations
{
    /// <inheritdoc />
    public partial class AddTasksTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TaskItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TaskItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TaskItems");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TaskItems",
                type: "TEXT",
                nullable: true);
        }
    }
}
