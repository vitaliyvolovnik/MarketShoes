using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    public partial class RemoveSellerAndCustomerModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketElements_Baskets_BasketId",
                table: "BasketElements");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketElements_Orders_OrderId",
                table: "BasketElements");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Customers_CustomerId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Sellers_SellerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sellers_SellerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCharacteristics_BasketElements_BasketElementId",
                table: "SubCharacteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCharacteristics_Products_ProductId",
                table: "SubCharacteristics");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Sellers");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_BasketElements_OrderId",
                table: "BasketElements");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "BasketElements");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "SubCharacteristics",
                newName: "OrderItemId");

            migrationBuilder.RenameColumn(
                name: "BasketElementId",
                table: "SubCharacteristics",
                newName: "BasketItemId");

            migrationBuilder.RenameIndex(
                name: "IX_SubCharacteristics_ProductId",
                table: "SubCharacteristics",
                newName: "IX_SubCharacteristics_OrderItemId");

            migrationBuilder.RenameIndex(
                name: "IX_SubCharacteristics_BasketElementId",
                table: "SubCharacteristics",
                newName: "IX_SubCharacteristics_BasketItemId");

            migrationBuilder.RenameColumn(
                name: "BasketId",
                table: "BasketElements",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketElements_BasketId",
                table: "BasketElements",
                newName: "IX_BasketElements_CustomerId");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItem_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInfos",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInfos", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_PersonalInfos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubCharacteristic_ProductsId",
                table: "ProductSubCharacteristic",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketElements_Users_CustomerId",
                table: "BasketElements",
                column: "CustomerId",
                principalTable: "Users",
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
                name: "FK_Orders_Users_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_SellerId",
                table: "Orders",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_SellerId",
                table: "Products",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCharacteristics_BasketElements_BasketItemId",
                table: "SubCharacteristics",
                column: "BasketItemId",
                principalTable: "BasketElements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCharacteristics_OrderItem_OrderItemId",
                table: "SubCharacteristics",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketElements_Users_CustomerId",
                table: "BasketElements");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Users_CustomerId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_SellerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_SellerId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCharacteristics_BasketElements_BasketItemId",
                table: "SubCharacteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCharacteristics_OrderItem_OrderItemId",
                table: "SubCharacteristics");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "PersonalInfos");

            migrationBuilder.DropTable(
                name: "ProductSubCharacteristic");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "OrderItemId",
                table: "SubCharacteristics",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "BasketItemId",
                table: "SubCharacteristics",
                newName: "BasketElementId");

            migrationBuilder.RenameIndex(
                name: "IX_SubCharacteristics_OrderItemId",
                table: "SubCharacteristics",
                newName: "IX_SubCharacteristics_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SubCharacteristics_BasketItemId",
                table: "SubCharacteristics",
                newName: "IX_SubCharacteristics_BasketElementId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "BasketElements",
                newName: "BasketId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketElements_CustomerId",
                table: "BasketElements",
                newName: "IX_BasketElements_BasketId");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "BasketElements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sellers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasketId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketElements_OrderId",
                table: "BasketElements",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BasketId",
                table: "Customers",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_UserId",
                table: "Sellers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketElements_Baskets_BasketId",
                table: "BasketElements",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketElements_Orders_OrderId",
                table: "BasketElements",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Customers_CustomerId",
                table: "Feedbacks",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Sellers_SellerId",
                table: "Orders",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sellers_SellerId",
                table: "Products",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCharacteristics_BasketElements_BasketElementId",
                table: "SubCharacteristics",
                column: "BasketElementId",
                principalTable: "BasketElements",
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
