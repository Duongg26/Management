using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChucNang_Accounts_AccountId",
                table: "ChucNang");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Accounts_AccountId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_ChucNang_PhanQuyenId",
                table: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_PhanQuyenId",
                table: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChucNang",
                table: "ChucNang");

            migrationBuilder.DropColumn(
                name: "PhanQuyenId",
                table: "Permissions");

            migrationBuilder.RenameTable(
                name: "Permissions",
                newName: "Functions");

            migrationBuilder.RenameTable(
                name: "ChucNang",
                newName: "PhanQuyen");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_AccountId",
                table: "Functions",
                newName: "IX_Functions_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_ChucNang_AccountId",
                table: "PhanQuyen",
                newName: "IX_PhanQuyen_AccountId");

            migrationBuilder.AddColumn<int>(
                name: "FunctionsId",
                table: "PhanQuyen",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Functions",
                table: "Functions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhanQuyen",
                table: "PhanQuyen",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PhanQuyen_FunctionsId",
                table: "PhanQuyen",
                column: "FunctionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Functions_Accounts_AccountId",
                table: "Functions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanQuyen_Accounts_AccountId",
                table: "PhanQuyen",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanQuyen_Functions_FunctionsId",
                table: "PhanQuyen",
                column: "FunctionsId",
                principalTable: "Functions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Functions_Accounts_AccountId",
                table: "Functions");

            migrationBuilder.DropForeignKey(
                name: "FK_PhanQuyen_Accounts_AccountId",
                table: "PhanQuyen");

            migrationBuilder.DropForeignKey(
                name: "FK_PhanQuyen_Functions_FunctionsId",
                table: "PhanQuyen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhanQuyen",
                table: "PhanQuyen");

            migrationBuilder.DropIndex(
                name: "IX_PhanQuyen_FunctionsId",
                table: "PhanQuyen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Functions",
                table: "Functions");

            migrationBuilder.DropColumn(
                name: "FunctionsId",
                table: "PhanQuyen");

            migrationBuilder.RenameTable(
                name: "PhanQuyen",
                newName: "ChucNang");

            migrationBuilder.RenameTable(
                name: "Functions",
                newName: "Permissions");

            migrationBuilder.RenameIndex(
                name: "IX_PhanQuyen_AccountId",
                table: "ChucNang",
                newName: "IX_ChucNang_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Functions_AccountId",
                table: "Permissions",
                newName: "IX_Permissions_AccountId");

            migrationBuilder.AddColumn<int>(
                name: "PhanQuyenId",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChucNang",
                table: "ChucNang",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                table: "Permissions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PhanQuyenId",
                table: "Permissions",
                column: "PhanQuyenId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChucNang_Accounts_AccountId",
                table: "ChucNang",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Accounts_AccountId",
                table: "Permissions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_ChucNang_PhanQuyenId",
                table: "Permissions",
                column: "PhanQuyenId",
                principalTable: "ChucNang",
                principalColumn: "Id");
        }
    }
}
