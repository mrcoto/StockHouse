using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StockHouse.Migrations
{
    public partial class SchemaCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    alias_name = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    is_item = table.Column<bool>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "warehouse",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "warehouse_has_product",
                columns: table => new
                {
                    warehouse_id = table.Column<int>(nullable: false),
                    product_id = table.Column<int>(nullable: false),
                    stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse_has_product", x => new { x.warehouse_id, x.product_id });
                });

            migrationBuilder.CreateTable(
                name: "product_has_product",
                columns: table => new
                {
                    product_id = table.Column<int>(nullable: false),
                    product_content_id = table.Column<int>(nullable: false),
                    quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_has_product", x => new { x.product_id, x.product_content_id });
                    table.ForeignKey(
                        name: "FK_product_has_product_product_product_content_id",
                        column: x => x.product_content_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_has_product_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_has_product_product_content_id",
                table: "product_has_product",
                column: "product_content_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_has_product");

            migrationBuilder.DropTable(
                name: "warehouse");

            migrationBuilder.DropTable(
                name: "warehouse_has_product");

            migrationBuilder.DropTable(
                name: "product");
        }
    }
}
