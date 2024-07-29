using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Migrations
{
    /// <inheritdoc />
    public partial class add29071 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGroup",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "NguoiLam",
                table: "Works");

            migrationBuilder.CreateTable(
                name: "NguoiLam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkId = table.Column<int>(type: "int", nullable: false),
                    IdNguoiLam = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiLam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NguoiLam_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NguoiLam_WorkId",
                table: "NguoiLam",
                column: "WorkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NguoiLam");

            migrationBuilder.AddColumn<int>(
                name: "IsGroup",
                table: "Works",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NguoiLam",
                table: "Works",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
