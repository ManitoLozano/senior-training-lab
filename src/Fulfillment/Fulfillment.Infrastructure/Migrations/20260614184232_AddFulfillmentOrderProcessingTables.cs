using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fulfillment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFulfillmentOrderProcessingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fulfillment");

            migrationBuilder.CreateTable(
                name: "OrderProcessing",
                schema: "fulfillment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProcessing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "order_processing_histories",
                schema: "fulfillment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderProcessingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_processing_histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_processing_histories_OrderProcessing_OrderProcessingId",
                        column: x => x.OrderProcessingId,
                        principalSchema: "fulfillment",
                        principalTable: "OrderProcessing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_processing_histories_OrderProcessingId",
                schema: "fulfillment",
                table: "order_processing_histories",
                column: "OrderProcessingId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProcessing_OrderId",
                schema: "fulfillment",
                table: "OrderProcessing",
                column: "OrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_processing_histories",
                schema: "fulfillment");

            migrationBuilder.DropTable(
                name: "OrderProcessing",
                schema: "fulfillment");
        }
    }
}
