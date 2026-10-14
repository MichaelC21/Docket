using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Docket.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitJobTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Handler = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Payload = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Attempted = table.Column<int>(type: "int", nullable: false, defaultValue: 3),
                    MaxAttempts = table.Column<int>(type: "int", nullable: false),
                    NextRunUTC = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAtUTC = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
