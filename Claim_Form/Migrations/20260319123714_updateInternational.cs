using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Claim_Form.Migrations
{
    /// <inheritdoc />
    public partial class updateInternational : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashInfo_TravelDetails_id",
                table: "CashInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelDetails_RecentClaims_RecentClaimId",
                table: "TravelDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "TravelDetailsTravelID",
                table: "CashInfo",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CashInfo_TravelDetailsTravelID",
                table: "CashInfo",
                column: "TravelDetailsTravelID");

            migrationBuilder.AddForeignKey(
                name: "FK_CashInfo_TravelDetails_TravelDetailsTravelID",
                table: "CashInfo",
                column: "TravelDetailsTravelID",
                principalTable: "TravelDetails",
                principalColumn: "TravelID");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelDetails_RecentClaims_RecentClaimId",
                table: "TravelDetails",
                column: "RecentClaimId",
                principalTable: "RecentClaims",
                principalColumn: "RecentClaimId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashInfo_TravelDetails_TravelDetailsTravelID",
                table: "CashInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_TravelDetails_RecentClaims_RecentClaimId",
                table: "TravelDetails");

            migrationBuilder.DropIndex(
                name: "IX_CashInfo_TravelDetailsTravelID",
                table: "CashInfo");

            migrationBuilder.DropColumn(
                name: "TravelDetailsTravelID",
                table: "CashInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_CashInfo_TravelDetails_id",
                table: "CashInfo",
                column: "id",
                principalTable: "TravelDetails",
                principalColumn: "TravelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TravelDetails_RecentClaims_RecentClaimId",
                table: "TravelDetails",
                column: "RecentClaimId",
                principalTable: "RecentClaims",
                principalColumn: "RecentClaimId");
        }
    }
}
