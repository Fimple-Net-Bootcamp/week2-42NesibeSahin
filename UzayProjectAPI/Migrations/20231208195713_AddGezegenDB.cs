using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UzayProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddGezegenDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Uydular",
                columns: new[] { "ID", "Derece", "GezegenID", "UyduAdi" },
                values: new object[,]
                {
                    { 1, 1m, 1, "Uydular A" },
                    { 2, 2m, 1, "Uydular B" },
                    { 3, 3m, 2, "Uydular C" },
                    { 4, 4m, 2, "Uydular D" },
                    { 5, 5m, 3, "Uydular E" },
                    { 6, 6m, 2, "Uydular F" },
                    { 7, 7m, 2, "Uydular G" },
                    { 8, 8m, 3, "Uydular H" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Uydular",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Uydular",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Uydular",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Uydular",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Uydular",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Uydular",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Uydular",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Uydular",
                keyColumn: "ID",
                keyValue: 8);
        }
    }
}
