using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Claim_Form.Migrations
{
    /// <inheritdoc />
    public partial class TravelModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelDetails_RecentClaims_RecentClaimId",
                table: "TravelDetails");

            migrationBuilder.DropTable(
                name: "CashDetails");

            migrationBuilder.DropIndex(
                name: "IX_TravelDetails_RecentClaimId",
                table: "TravelDetails");

            migrationBuilder.CreateTable(
                name: "CashInfo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoadedDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    INRRate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalLoaded = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashInfo", x => x.id);
                    table.ForeignKey(
                        name: "FK_CashInfo_TravelDetails_id",
                        column: x => x.id,
                        principalTable: "TravelDetails",
                        principalColumn: "TravelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TravelDetails_RecentClaimId",
                table: "TravelDetails",
                column: "RecentClaimId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelDetails_RecentClaims_RecentClaimId",
                table: "TravelDetails",
                column: "RecentClaimId",
                principalTable: "RecentClaims",
                principalColumn: "RecentClaimId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelDetails_RecentClaims_RecentClaimId",
                table: "TravelDetails");

            migrationBuilder.DropTable(
                name: "CashInfo");

            migrationBuilder.DropIndex(
                name: "IX_TravelDetails_RecentClaimId",
                table: "TravelDetails");

            migrationBuilder.CreateTable(
                name: "CashDetails",
                columns: table => new
                {
                    TravelDetailsTravelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    INRRate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoadedDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalLoaded = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_TravelDetails_RecentClaims_RecentClaimId",
                table: "TravelDetails",
                column: "RecentClaimId",
                principalTable: "RecentClaims",
                principalColumn: "RecentClaimId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
