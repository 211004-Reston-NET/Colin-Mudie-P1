using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Logic.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    price = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    description = table.Column<string>(type: "varchar(750)", unicode: false, maxLength: 750, nullable: false),
                    brand = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false),
                    category = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.product_id);
                });

            migrationBuilder.CreateTable(
                name: "storefront",
                columns: table => new
                {
                    storefront_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storefront", x => x.storefront_id);
                });

            migrationBuilder.CreateTable(
                name: "line_item",
                columns: table => new
                {
                    line_item_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    storefront_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_line_item", x => x.line_item_id);
                    table.ForeignKey(
                        name: "FK__line_item__produ__160F4887",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__line_item__store__17036CC0",
                        column: x => x.storefront_id,
                        principalTable: "storefront",
                        principalColumn: "storefront_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    storefront_id = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_", x => x.order_id);
                    table.ForeignKey(
                        name: "FK__order___storefro__1332DBDC",
                        column: x => x.storefront_id,
                        principalTable: "storefront",
                        principalColumn: "storefront_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "order__FK",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LineItemsOrder",
                columns: table => new
                {
                    LineItemsId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItemsOrder", x => new { x.LineItemsId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_LineItemsOrder_line_item_LineItemsId",
                        column: x => x.LineItemsId,
                        principalTable: "line_item",
                        principalColumn: "line_item_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItemsOrder_order__OrderId",
                        column: x => x.OrderId,
                        principalTable: "order_",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__customer__AB6E61642D925A68",
                table: "customer",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_line_item_product_id",
                table: "line_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_line_item_storefront_id",
                table: "line_item",
                column: "storefront_id");

            migrationBuilder.CreateIndex(
                name: "IX_LineItemsOrder_OrderId",
                table: "LineItemsOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_order__customer_id",
                table: "order_",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_order__storefront_id",
                table: "order_",
                column: "storefront_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineItemsOrder");

            migrationBuilder.DropTable(
                name: "line_item");

            migrationBuilder.DropTable(
                name: "order_");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "storefront");

            migrationBuilder.DropTable(
                name: "customer");
        }
    }
}
