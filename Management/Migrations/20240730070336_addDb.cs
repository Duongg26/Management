using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Migrations
{
    /// <inheritdoc />
    public partial class addDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DsNguoiLam",
                table: "Works",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DsTep",
                table: "Works",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenCV",
                table: "Works",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Teps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teps_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teps_WorkId",
                table: "Teps",
                column: "WorkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teps");

            migrationBuilder.DropColumn(
                name: "DsNguoiLam",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "DsTep",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "TenCV",
                table: "Works");
        }
    }
}
