using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Claim_Form.Migrations
{
    /// <inheritdoc />
    public partial class addedtheauditInfoa1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TravelDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TravelDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "TravelDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "TravelDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TravelDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TravelDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "TravelDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TravelDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RecentClaims",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "RecentClaims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "RecentClaims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "RecentClaims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RecentClaims",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RecentClaims",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "RecentClaims",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "RecentClaims",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Internationals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Internationals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Internationals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Internationals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Internationals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Internationals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Internationals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Domestics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Domestics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Domestics",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Domestics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Domestics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Domestics",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Domestics",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TravelDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TravelDetails");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "TravelDetails");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "TravelDetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TravelDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TravelDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "TravelDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TravelDetails");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RecentClaims");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "RecentClaims");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "RecentClaims");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "RecentClaims");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecentClaims");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RecentClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "RecentClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "RecentClaims");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Internationals");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Internationals");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Internationals");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Internationals");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Internationals");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Internationals");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Internationals");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Domestics");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Domestics");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Domestics");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Domestics");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Domestics");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Domestics");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Domestics");
        }
    }
}
