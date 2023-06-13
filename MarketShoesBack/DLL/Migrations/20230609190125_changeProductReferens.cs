using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class changeProductReferens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSubCharacteristic");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "SubCharacteristics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCharacteristics_ProductId",
                table: "SubCharacteristics",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCharacteristics_Products_ProductId",
                table: "SubCharacteristics",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCharacteristics_Products_ProductId",
                table: "SubCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_SubCharacteristics_ProductId",
                table: "SubCharacteristics");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SubCharacteristics");

            migrationBuilder.CreateTable(
                name: "ProductSubCharacteristic",
                columns: table => new
                {
                    CharacteristicsId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSubCharacteristic", x => new { x.CharacteristicsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductSubCharacteristic_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSubCharacteristic_SubCharacteristics_CharacteristicsId",
                        column: x => x.CharacteristicsId,
                        principalTable: "SubCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubCharacteristic_ProductsId",
                table: "ProductSubCharacteristic",
                column: "ProductsId");
        }
    }
}
