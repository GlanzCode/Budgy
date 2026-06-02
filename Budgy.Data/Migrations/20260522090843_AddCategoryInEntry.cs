using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budgy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryInEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Entries",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entries_CategoryId",
                table: "Entries",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Categories_CategoryId",
                table: "Entries",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Categories_CategoryId",
                table: "Entries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_CategoryId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Entries");
        }
    }
}
