using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitCategorySubcategoryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorySubcategories_Subcategories_SubcategoryId",
                table: "CategorySubcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategorySubcategories",
                table: "CategorySubcategories");

            migrationBuilder.DropIndex(
                name: "IX_CategorySubcategories_CategoryId",
                table: "CategorySubcategories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7400f3cf-6509-4d59-a399-538e783eb07d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bdeec34-6284-4f17-a318-aba6ecff39cb");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CategorySubcategories");

            migrationBuilder.AlterColumn<int>(
                name: "SubcategoryId",
                table: "CategorySubcategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategorySubcategories",
                table: "CategorySubcategories",
                columns: new[] { "CategoryId", "SubcategoryId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4e738aa3-9613-40a7-8871-5c491f13ece9", null, "user", "USER" },
                    { "d98eeb3a-5935-4d93-b158-e5a10c4d776a", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySubcategories_Subcategories_SubcategoryId",
                table: "CategorySubcategories",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorySubcategories_Subcategories_SubcategoryId",
                table: "CategorySubcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategorySubcategories",
                table: "CategorySubcategories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e738aa3-9613-40a7-8871-5c491f13ece9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d98eeb3a-5935-4d93-b158-e5a10c4d776a");

            migrationBuilder.AlterColumn<int>(
                name: "SubcategoryId",
                table: "CategorySubcategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CategorySubcategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategorySubcategories",
                table: "CategorySubcategories",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7400f3cf-6509-4d59-a399-538e783eb07d", null, "user", "USER" },
                    { "7bdeec34-6284-4f17-a318-aba6ecff39cb", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategorySubcategories_CategoryId",
                table: "CategorySubcategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorySubcategories_Subcategories_SubcategoryId",
                table: "CategorySubcategories",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");
        }
    }
}
