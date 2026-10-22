using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace data_storage_website_ap5pw.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class datastorage_104_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carousel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ImageSrc = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageAlt = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carousel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Carousel",
                columns: new[] { "Id", "ImageAlt", "ImageSrc" },
                values: new object[,]
                {
                    { 1, "First slide", "/img/carousel/information-technology-specialist.jpg" },
                    { 2, "Second slide", "/img/carousel/Information-Technology-1-1.jpg" },
                    { 3, "Third slide", "/img/carousel/itec-index-banner.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "FileName", "Size", "StoragePath", "UserId" },
                values: new object[,]
                {
                    { 1, "test1", 1L, "/test/1", 0 },
                    { 2, "test2", 2L, "/test/2", 0 },
                    { 3, "test3", 3L, "/test/3", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carousel");

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
