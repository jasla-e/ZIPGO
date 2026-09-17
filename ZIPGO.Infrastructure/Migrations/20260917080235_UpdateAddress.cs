using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZIPGO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresss_Users_UserId",
                table: "Addresss");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresss",
                table: "Addresss");

            migrationBuilder.RenameTable(
                name: "Addresss",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_Addresss_UserId",
                table: "Addresses",
                newName: "IX_Addresses_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_UserId",
                table: "Addresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_UserId",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Addresss");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_UserId",
                table: "Addresss",
                newName: "IX_Addresss_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresss",
                table: "Addresss",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresss_Users_UserId",
                table: "Addresss",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
