using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Claim_Form.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TravelDetails",
                newName: "TravelId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RecentClaims",
                newName: "RecentClaimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TravelId",
                table: "TravelDetails",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RecentClaimId",
                table: "RecentClaims",
                newName: "Id");
        }
    }
}
