using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Migrations
{
    /// <inheritdoc />
    public partial class add29072 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateWork",
                table: "Works");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Works",
                newName: "TepDinhKemPath");

            migrationBuilder.AddColumn<DateOnly>(
                name: "NgayGiao",
                table: "Works",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "NgayXong",
                table: "Works",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "NoiDung",
                table: "Works",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayGiao",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "NgayXong",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "NoiDung",
                table: "Works");

            migrationBuilder.RenameColumn(
                name: "TepDinhKemPath",
                table: "Works",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateWork",
                table: "Works",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
