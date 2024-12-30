using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sqruffle.Data.Migrations
{
    /// <inheritdoc />
    public partial class AspectIntroduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "AProductAspectSequence");

            migrationBuilder.CreateTable(
                name: "Product_Expires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"AProductAspectSequence\"')"),
                    Type = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Expires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Expires_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_OwnershipRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"AProductAspectSequence\"')"),
                    Type = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegisterAt = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_OwnershipRegistration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_OwnershipRegistration_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_PeriodicYield",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('\"AProductAspectSequence\"')"),
                    Type = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Interval = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Increase = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_PeriodicYield", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_PeriodicYield_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_Expires_ProductId",
                table: "Product_Expires",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_OwnershipRegistration_ProductId",
                table: "Product_OwnershipRegistration",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_PeriodicYield_ProductId",
                table: "Product_PeriodicYield",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product_Expires");

            migrationBuilder.DropTable(
                name: "Product_OwnershipRegistration");

            migrationBuilder.DropTable(
                name: "Product_PeriodicYield");

            migrationBuilder.DropSequence(
                name: "AProductAspectSequence");
        }
    }
}
