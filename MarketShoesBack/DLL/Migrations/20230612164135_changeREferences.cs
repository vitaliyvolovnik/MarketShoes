using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class changeREferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Products_ProductId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Users_CustomerId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Feedbacks_FeedbackId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCharacteristics_Products_ProductId",
                table: "SubCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_SubCharacteristics_ProductId",
                table: "SubCharacteristics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SubCharacteristics");

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                newName: "Feedback");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_ProductId",
                table: "Feedback",
                newName: "IX_Feedback_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_CustomerId",
                table: "Feedback",
                newName: "IX_Feedback_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Products_ProductId",
                table: "Feedback",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Users_CustomerId",
                table: "Feedback",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Feedback_FeedbackId",
                table: "Photo",
                column: "FeedbackId",
                principalTable: "Feedback",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Products_ProductId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Users_CustomerId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Feedback_FeedbackId",
                table: "Photo");

            migrationBuilder.DropTable(
                name: "ProductSubCharacteristic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback");

            migrationBuilder.RenameTable(
                name: "Feedback",
                newName: "Feedbacks");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_ProductId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_CustomerId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_CustomerId");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "SubCharacteristics",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SubCharacteristics_ProductId",
                table: "SubCharacteristics",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Products_ProductId",
                table: "Feedbacks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Users_CustomerId",
                table: "Feedbacks",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Feedbacks_FeedbackId",
                table: "Photo",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCharacteristics_Products_ProductId",
                table: "SubCharacteristics",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
