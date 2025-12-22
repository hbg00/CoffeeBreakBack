using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeBreakAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PriceCurrency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    ProductKind = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Cake_Type = table.Column<int>(type: "int", nullable: true),
                    IsGlutenFree = table.Column<bool>(type: "bit", nullable: true),
                    Calories = table.Column<int>(type: "int", nullable: true),
                    Coffee_Type = table.Column<int>(type: "int", nullable: true),
                    Size = table.Column<int>(type: "int", nullable: true),
                    HasMilk = table.Column<bool>(type: "bit", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    HasChocolate = table.Column<bool>(type: "bit", nullable: true),
                    Tea_Type = table.Column<int>(type: "int", nullable: true),
                    IsHerbal = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
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
