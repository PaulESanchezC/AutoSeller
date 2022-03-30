using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Added_DeleteStatusToListedVehicleModel_andTo_IntentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateListed",
                table: "ListedVehicles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSold",
                table: "ListedVehicles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "ListedVehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSold",
                table: "Intents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Intents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateListed",
                table: "ListedVehicles");

            migrationBuilder.DropColumn(
                name: "DateSold",
                table: "ListedVehicles");

            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "ListedVehicles");

            migrationBuilder.DropColumn(
                name: "DateSold",
                table: "Intents");

            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Intents");
        }
    }
}
