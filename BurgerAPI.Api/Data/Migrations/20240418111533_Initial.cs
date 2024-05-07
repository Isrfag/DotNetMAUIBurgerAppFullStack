using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BurgerAPI.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Burgers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Burgers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BurgerOptions",
                columns: table => new
                {
                    BurgerId = table.Column<int>(type: "int", nullable: false),
                    Meat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Letuce = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Bacon = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Tomato = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FriedEgg = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RegOnion = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CacaramelizedOnion = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Sauce = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CheeseType = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BurgerOptions", x => new { x.BurgerId, x.Meat, x.Letuce, x.Bacon, x.CacaramelizedOnion, x.FriedEgg, x.RegOnion, x.Tomato, x.CheeseType, x.Sauce });
                    table.ForeignKey(
                        name: "FK_BurgerOptions_Burgers_BurgerId",
                        column: x => x.BurgerId,
                        principalTable: "Burgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    BurgerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Meat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Letuce = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Bacon = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Tomato = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FriedEgg = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RegOnion = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CacaramelizedOnion = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Sauce = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CheeseType = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Burgers",
                columns: new[] { "Id", "Image", "Name", "price" },
                values: new object[,]
                {
                    { 1, "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg1.jpg", "Delicious Mexican", 10.98 },
                    { 2, "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg2.jpg", "Classic Burger", 8.9900000000000002 },
                    { 3, "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg3.jpg", "Capra", 10.99 },
                    { 4, "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg4.jpg", "Cheese Madness", 11.5 },
                    { 5, "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg5.jpg", "Minimalist", 7.8499999999999996 },
                    { 6, "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg6.jpg", "Chicken Delight", 8.9499999999999993 },
                    { 7, "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg7.jpg", "San Francisco", 10.800000000000001 },
                    { 8, "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg8.jpg", "Full Meat", 13.25 },
                    { 9, "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg9.jpg", "Carbonara", 12.99 },
                    { 10, "https://raw.githubusercontent.com/Isrfag/Images-For-Projecrs/master/Burgers/bg10.jpg", "NoSurvive", 19.989999999999998 }
                });

            migrationBuilder.InsertData(
                table: "BurgerOptions",
                columns: new[] { "Bacon", "BurgerId", "CacaramelizedOnion", "CheeseType", "FriedEgg", "Letuce", "Meat", "RegOnion", "Sauce", "Tomato" },
                values: new object[,]
                {
                    { "yes", 1, "no", "none", "no", "no", "beef", "no", "guacamole", "no" },
                    { "no", 2, "no", "cheddar", "yes", "yes", "beef", "yes", "none", "yes" },
                    { "no", 3, "yes", "goat cheese", "no", "no", "beef", "no", "none", "no" },
                    { "no", 4, "no", "cheddar,camembert,roquefort,mozzarella", "no", "no", "Double Beef", "no", "cheddar sauce", "no" },
                    { "no", 5, "no", "cheddar", "no", "no", "beef", "no", "none", "no" },
                    { "no", 6, "no", "cheddar", "no", "yes", "chicken", "no", "mayonnaise", "yes" },
                    { "yes", 7, "no", "double cheddar", "yes", "no", "beef", "no", "mayonnaise", "no" },
                    { "yes", 8, "no", "cheddar", "no", "no", "double beef", "no", "none", "no" },
                    { "yes", 9, "no", "parmesan", "yes", "no", "beef", "yes", "cream", "no" },
                    { "yes", 10, "no", "quadruple cheddar", "no", "yes", "quadruple beef", "no", "cheese", "yes" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BurgerOptions");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Burgers");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
