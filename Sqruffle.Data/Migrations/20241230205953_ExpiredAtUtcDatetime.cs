using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sqruffle.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExpiredAtUtcDatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpiredAt",
                table: "Product_Expires",
                newName: "ExpiredAtUtc");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Product_Expires",
                newName: "ExpiresAtUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpiresAtUtc",
                table: "Product_Expires",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "ExpiredAtUtc",
                table: "Product_Expires",
                newName: "ExpiredAt");
        }
    }
}
