using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HipHopPizzaNWings.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Uid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerName = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateClosed = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    OrderTypeId = table.Column<int>(type: "integer", nullable: false),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    Subtotal = table.Column<decimal>(type: "numeric", nullable: false),
                    Tip = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_OrderTypes_OrderTypeId",
                        column: x => x.OrderTypeId,
                        principalTable: "OrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: true),
                    ItemId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Large Pepperoni Pizza", 12.00m },
                    { 2, "Large Supreme Pizza", 14.00m },
                    { 3, "Large Cheese Pizza", 10.00m },
                    { 4, "8 Piece Buffalo Wings", 7.00m },
                    { 5, "12 Piece Buffalo Wings", 11.00m }
                });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Online" },
                    { 2, "In Store" },
                    { 3, "Phone" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Uid" },
                values: new object[] { 1, "06mu1mmHWrMad67XQeCBnKyYKPw1" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerName", "DateClosed", "DateCreated", "Email", "IsClosed", "OrderTypeId", "PhoneNumber", "Subtotal", "Tip" },
                values: new object[,]
                {
                    { 1, "Mick Foley", null, new DateTime(2024, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "JLee@email.com", false, 1, "323-416-5523", 0m, 0m },
                    { 2, "Stan Hansen", new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "SHanson@email.com", true, 3, "323-512-0833", 23.00m, 5.00m },
                    { 3, "Brody Lee", null, new DateTime(2024, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "BrodyForever@email.com", false, 2, "323-723-9376", 0m, 0m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemId",
                table: "OrderItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "OrderTypes");
        }
    }
}
