using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZIPGO.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RedesignCategoryRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Create the new junction table
            migrationBuilder.CreateTable(
                name: "MainCategorySubCategory",
                columns: table => new
                {
                    MainCategoryId = table.Column<int>(
                        type: "int",
                        nullable: false),

                    SubCategoryId = table.Column<int>(
                        type: "int",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_MainCategorySubCategory",
                        x => new { x.MainCategoryId, x.SubCategoryId });

                    table.ForeignKey(
                        name: "FK_MainCategorySubCategory_MainCategory_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "MainCategory",
                        principalColumn: "Id",
                       onDelete: ReferentialAction.NoAction);

                    table.ForeignKey(
                        name: "FK_MainCategorySubCategory_SubCategory_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategory",
                        principalColumn: "Id",
                       onDelete: ReferentialAction.NoAction);
                });

            // 2. Copy existing SubCategory → MainCategory relationships
            migrationBuilder.Sql(@"
        INSERT INTO MainCategorySubCategory
            (MainCategoryId, SubCategoryId)
        SELECT
            MainCategoryId,
            Id
        FROM SubCategory
        WHERE MainCategoryId IS NOT NULL;
    ");

            // 3. Add MainCategoryId to Products
            migrationBuilder.AddColumn<int>(
                name: "MainCategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            // 4. Copy the correct MainCategoryId to existing Products
            migrationBuilder.Sql(@"
        UPDATE Products
        SET MainCategoryId = SubCategory.MainCategoryId
        FROM Products
        INNER JOIN SubCategory
            ON Products.SubCategoryId = SubCategory.Id;
    ");

            // 5. Make Product.MainCategoryId required
            migrationBuilder.AlterColumn<int>(
                name: "MainCategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // 6. Create indexes
            migrationBuilder.CreateIndex(
                name: "IX_Products_MainCategoryId",
                table: "Products",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MainCategorySubCategory_SubCategoryId",
                table: "MainCategorySubCategory",
                column: "SubCategoryId");

            // 7. Create Product → MainCategory foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Products_MainCategory_MainCategoryId",
                table: "Products",
                column: "MainCategoryId",
                principalTable: "MainCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            // 8. Remove old SubCategory → MainCategory foreign key
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_MainCategory_MainCategoryId",
                table: "SubCategory");

            migrationBuilder.DropIndex(
                name: "IX_SubCategory_MainCategoryId",
                table: "SubCategory");

            // 9. Finally remove the old column
            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "SubCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_MainCategory_MainCategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "MainCategorySubCategory");

            migrationBuilder.DropIndex(
                name: "IX_Products_MainCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "MainCategoryId",
                table: "SubCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_MainCategoryId",
                table: "SubCategory",
                column: "MainCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_MainCategory_MainCategoryId",
                table: "SubCategory",
                column: "MainCategoryId",
                principalTable: "MainCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
