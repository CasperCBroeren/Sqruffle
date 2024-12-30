using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sqruffle.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTypeInAProductFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Product_PeriodicYield");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Product_OwnershipRegistration");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Product_Expires");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Product_PeriodicYield",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Product_OwnershipRegistration",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Product_Expires",
                type: "text",
                nullable: true);
        }
    }
}
