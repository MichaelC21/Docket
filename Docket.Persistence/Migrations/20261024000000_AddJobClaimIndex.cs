using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Docket.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddJobClaimIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Jobs_Status_NextRunUTC_CreatedAtUTC",
                table: "Jobs",
                columns: new[] { "Status", "NextRunUTC", "CreatedAtUTC" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Jobs_Status_NextRunUTC_CreatedAtUTC",
                table: "Jobs");
        }
    }
}
