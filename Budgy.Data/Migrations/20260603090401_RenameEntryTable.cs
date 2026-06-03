using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budgy.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameEntryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Entries",
                newName: "EntryTemplate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "EntryTemplate",
                newName: "Entries");
        }
    }
}
