using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Claim_Form.Migrations
{
    /// <inheritdoc />
    public partial class International : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelDetails",
                columns: table => new
                {
                    TravelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrecncyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelStartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelEndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecentClaimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelDetails", x => x.TravelID);
                    table.ForeignKey(
                        name: "FK_TravelDetails_RecentClaims_RecentClaimId",
                        column: x => x.RecentClaimId,
                        principalTable: "RecentClaims",
                        principalColumn: "RecentClaimId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CashDetails",
                columns: table => new
                {
                    TravelDetailsTravelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoadedDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    INRRate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalLoaded = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashDetails", x => new { x.TravelDetailsTravelID, x.Id });
                    table.ForeignKey(
                        name: "FK_CashDetails_TravelDetails_TravelDetailsTravelID",
                        column: x => x.TravelDetailsTravelID,
                        principalTable: "TravelDetails",
                        principalColumn: "TravelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelDetails_RecentClaimId",
                table: "TravelDetails",
                column: "RecentClaimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashDetails");

            migrationBuilder.DropTable(
                name: "TravelDetails");
        }
    }
}
