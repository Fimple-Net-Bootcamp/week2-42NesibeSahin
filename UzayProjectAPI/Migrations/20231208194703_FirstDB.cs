using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UzayProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class FirstDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uzaylar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UzayAdi = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzaylar", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Gezegenler",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GezegenAdi = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UzayID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gezegenler", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Gezegenler_Uzaylar_UzayID",
                        column: x => x.UzayID,
                        principalTable: "Uzaylar",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uydular",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UyduAdi = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Derece = table.Column<decimal>(type: "TEXT", nullable: false),
                    GezegenID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uydular", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Uydular_Gezegenler_GezegenID",
                        column: x => x.GezegenID,
                        principalTable: "Gezegenler",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Uzaylar",
                columns: new[] { "ID", "UzayAdi" },
                values: new object[,]
                {
                    { 1, "Uzay A" },
                    { 2, "Uzay B" },
                    { 3, "Uzay C" }
                });

            migrationBuilder.InsertData(
                table: "Gezegenler",
                columns: new[] { "ID", "GezegenAdi", "UzayID" },
                values: new object[,]
                {
                    { 1, "Gezegen A", 1 },
                    { 2, "Gezegen B", 1 },
                    { 3, "Gezegen C", 2 },
                    { 4, "Gezegen D", 2 },
                    { 5, "Gezegen E", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gezegenler_UzayID",
                table: "Gezegenler",
                column: "UzayID");

            migrationBuilder.CreateIndex(
                name: "IX_Uydular_GezegenID",
                table: "Uydular",
                column: "GezegenID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uydular");

            migrationBuilder.DropTable(
                name: "Gezegenler");

            migrationBuilder.DropTable(
                name: "Uzaylar");
        }
    }
}
