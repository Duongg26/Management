using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhanQuyen_Accounts_AccountId",
                table: "PhanQuyen");

            migrationBuilder.DropForeignKey(
                name: "FK_PhanQuyen_Functions_FunctionsId",
                table: "PhanQuyen");

            migrationBuilder.AlterColumn<int>(
                name: "FunctionsId",
                table: "PhanQuyen",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "PhanQuyen",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Addr",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PhanQuyen_Accounts_AccountId",
                table: "PhanQuyen",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhanQuyen_Functions_FunctionsId",
                table: "PhanQuyen",
                column: "FunctionsId",
                principalTable: "Functions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhanQuyen_Accounts_AccountId",
                table: "PhanQuyen");

            migrationBuilder.DropForeignKey(
                name: "FK_PhanQuyen_Functions_FunctionsId",
                table: "PhanQuyen");

            migrationBuilder.DropColumn(
                name: "Addr",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "FunctionsId",
                table: "PhanQuyen",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "PhanQuyen",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
