using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductCatalog.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    StockQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "Name", "Price", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Electronics", new DateTime(2026, 1, 8, 2, 18, 41, 427, DateTimeKind.Utc).AddTicks(5995), "High-performance laptop for development", "Laptop", 1299.99m, 50, new DateTime(2026, 1, 8, 2, 18, 41, 427, DateTimeKind.Utc).AddTicks(5997) },
                    { 2, "Accessories", new DateTime(2026, 1, 8, 2, 18, 41, 427, DateTimeKind.Utc).AddTicks(6003), "Ergonomic wireless mouse", "Wireless Mouse", 29.99m, 200, new DateTime(2026, 1, 8, 2, 18, 41, 427, DateTimeKind.Utc).AddTicks(6003) },
                    { 3, "Accessories", new DateTime(2026, 1, 8, 2, 18, 41, 427, DateTimeKind.Utc).AddTicks(6005), "RGB mechanical keyboard", "Mechanical Keyboard", 149.99m, 75, new DateTime(2026, 1, 8, 2, 18, 41, 427, DateTimeKind.Utc).AddTicks(6005) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
