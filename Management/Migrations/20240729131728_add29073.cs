using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Migrations
{
    /// <inheritdoc />
    public partial class add29073 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
        name: "NoiDung",
        table: "Works",
        newName: "NoiDungTemp");

            // Đổi tên cột "TepDinhKemPath" thành "NoiDung"
            migrationBuilder.RenameColumn(
                name: "TepDinhKemPath",
                table: "Works",
                newName: "NoiDung");

            // Đổi tên cột "NoiDungTemp" thành "TepDinhKemPath"
            migrationBuilder.RenameColumn(
                name: "NoiDungTemp",
                table: "Works",
                newName: "TepDinhKemPath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
