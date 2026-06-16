using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fulfillment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderProcessingItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderProcessingItems",
                schema: "fulfillment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderProcessingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProcessingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProcessingItems_OrderProcessing_OrderProcessingId",
                        column: x => x.OrderProcessingId,
                        principalSchema: "fulfillment",
                        principalTable: "OrderProcessing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProcessingItems_OrderProcessingId",
                schema: "fulfillment",
                table: "OrderProcessingItems",
                column: "OrderProcessingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProcessingItems",
                schema: "fulfillment");
        }
    }
}
