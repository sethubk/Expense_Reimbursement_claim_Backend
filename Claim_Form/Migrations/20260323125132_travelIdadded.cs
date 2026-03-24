using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Claim_Form.Migrations
{
    /// <inheritdoc />
    public partial class travelIdadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashInfo_TravelDetails_id",
                table: "CashInfo");

            migrationBuilder.AddColumn<Guid>(
                name: "TravelId",
                table: "CashInfo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CashInfo_TravelId",
                table: "CashInfo",
                column: "TravelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashInfo_TravelDetails_TravelId",
                table: "CashInfo",
                column: "TravelId",
                principalTable: "TravelDetails",
                principalColumn: "TravelID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashInfo_TravelDetails_TravelId",
                table: "CashInfo");

            migrationBuilder.DropIndex(
                name: "IX_CashInfo_TravelId",
                table: "CashInfo");

            migrationBuilder.DropColumn(
                name: "TravelId",
                table: "CashInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_CashInfo_TravelDetails_id",
                table: "CashInfo",
                column: "id",
                principalTable: "TravelDetails",
                principalColumn: "TravelID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
