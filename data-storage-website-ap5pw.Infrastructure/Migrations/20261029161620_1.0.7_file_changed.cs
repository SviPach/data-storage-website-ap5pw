using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace data_storage_website_ap5pw.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _107_file_changed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Files",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "StoragePath",
                table: "Files",
                newName: "Text");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Files",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldMaxLength: 70)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "Files",
                type: "varchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AuthorId", "FileName", "Text", "Topic" },
                values: new object[] { 1, "file1", "Some text about topic1.", "Topic1" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AuthorId", "FileName", "Text", "Topic" },
                values: new object[] { 1, "file2", "Some text about topic2.", "Topic2" });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AuthorId", "FileName", "Text", "Topic" },
                values: new object[] { 1, "file3", "Some text about topic3.", "Topic3" });

            migrationBuilder.CreateIndex(
                name: "IX_Files_AuthorId",
                table: "Files",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_AspNetUsers_AuthorId",
                table: "Files",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_AspNetUsers_AuthorId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_AuthorId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Files",
                newName: "StoragePath");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Files",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Files",
                type: "varchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "Files",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FileName", "Size", "StoragePath", "UserId" },
                values: new object[] { "test1", 1L, "/test/1", 0 });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FileName", "Size", "StoragePath", "UserId" },
                values: new object[] { "test2", 2L, "/test/2", 0 });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FileName", "Size", "StoragePath", "UserId" },
                values: new object[] { "test3", 3L, "/test/3", 0 });
        }
    }
}
