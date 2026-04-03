using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Claim_Form.Migrations
{
    /// <inheritdoc />
    public partial class allModelID_change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashInfo_TravelDetails_TravelId",
                table: "CashInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_RecentClaims_Employees_EmpId",
                table: "RecentClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashInfo",
                table: "CashInfo");

            migrationBuilder.DropColumn(
                name: "TotalLoaded",
                table: "CashInfo");

            migrationBuilder.RenameTable(
                name: "CashInfo",
                newName: "CashInfos");

            migrationBuilder.RenameColumn(
                name: "ReimbersementStatus",
                table: "TravelDetails",
                newName: "ReimbursementStatus");

            migrationBuilder.RenameColumn(
                name: "CurrecncyType",
                table: "TravelDetails",
                newName: "CurrencyType");

            migrationBuilder.RenameColumn(
                name: "TravelID",
                table: "TravelDetails",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RecentClaimId",
                table: "RecentClaims",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "InternationalId",
                table: "Internationals",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ExpenseId",
                table: "Expenses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "passwordHash",
                table: "Employees",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "VenderCost",
                table: "Employees",
                newName: "VendorCost");

            migrationBuilder.RenameColumn(
                name: "INRRate",
                table: "CashInfos",
                newName: "InrRate");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CashInfos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "CashInfos",
                newName: "PaymentType");

            migrationBuilder.RenameIndex(
                name: "IX_CashInfo_TravelId",
                table: "CashInfos",
                newName: "IX_CashInfos_TravelId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TravelStartDate",
                table: "TravelDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TravelEndDate",
                table: "TravelDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "TotalDays",
                table: "TravelDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdvanceAmount",
                table: "TravelDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LoadedDate",
                table: "CashInfos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "InrRate",
                table: "CashInfos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalLoadedAmount",
                table: "CashInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashInfos",
                table: "CashInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashInfos_TravelDetails_TravelId",
                table: "CashInfos",
                column: "TravelId",
                principalTable: "TravelDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecentClaims_Employees_EmpId",
                table: "RecentClaims",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashInfos_TravelDetails_TravelId",
                table: "CashInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_RecentClaims_Employees_EmpId",
                table: "RecentClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashInfos",
                table: "CashInfos");

            migrationBuilder.DropColumn(
                name: "TotalLoadedAmount",
                table: "CashInfos");

            migrationBuilder.RenameTable(
                name: "CashInfos",
                newName: "CashInfo");

            migrationBuilder.RenameColumn(
                name: "ReimbursementStatus",
                table: "TravelDetails",
                newName: "ReimbersementStatus");

            migrationBuilder.RenameColumn(
                name: "CurrencyType",
                table: "TravelDetails",
                newName: "CurrecncyType");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TravelDetails",
                newName: "TravelID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RecentClaims",
                newName: "RecentClaimId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Internationals",
                newName: "InternationalId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Expenses",
                newName: "ExpenseId");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Employees",
                newName: "passwordHash");

            migrationBuilder.RenameColumn(
                name: "VendorCost",
                table: "Employees",
                newName: "VenderCost");

            migrationBuilder.RenameColumn(
                name: "InrRate",
                table: "CashInfo",
                newName: "INRRate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CashInfo",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PaymentType",
                table: "CashInfo",
                newName: "Type");

            migrationBuilder.RenameIndex(
                name: "IX_CashInfos_TravelId",
                table: "CashInfo",
                newName: "IX_CashInfo_TravelId");

            migrationBuilder.AlterColumn<string>(
                name: "TravelStartDate",
                table: "TravelDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "TravelEndDate",
                table: "TravelDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "TotalDays",
                table: "TravelDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AdvanceAmount",
                table: "TravelDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "LoadedDate",
                table: "CashInfo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "INRRate",
                table: "CashInfo",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "TotalLoaded",
                table: "CashInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashInfo",
                table: "CashInfo",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CashInfo_TravelDetails_TravelId",
                table: "CashInfo",
                column: "TravelId",
                principalTable: "TravelDetails",
                principalColumn: "TravelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecentClaims_Employees_EmpId",
                table: "RecentClaims",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
