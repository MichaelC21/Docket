using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Docket.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByAndCreatedAtColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ClaimedAt",
                table: "Jobs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClaimedBy",
                table: "Jobs",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimedAt",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ClaimedBy",
                table: "Jobs");
        }
    }
}
