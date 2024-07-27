using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Migrations
{
    /// <inheritdoc />
    public partial class addDbnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhanQuyen_Accounts_AccountId",
                table: "PhanQuyen");

            migrationBuilder.DropIndex(
                name: "IX_PhanQuyen_AccountId",
                table: "PhanQuyen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyen_AccountId",
                table: "PhanQuyen",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanQuyen_Accounts_AccountId",
                table: "PhanQuyen",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
