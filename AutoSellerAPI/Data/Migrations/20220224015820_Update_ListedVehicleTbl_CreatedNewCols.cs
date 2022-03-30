using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Update_ListedVehicleTbl_CreatedNewCols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriveTrain",
                table: "ListedVehicles",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Mileage",
                table: "ListedVehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "ListedVehicles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "ListedVehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriveTrain",
                table: "ListedVehicles");

            migrationBuilder.DropColumn(
                name: "Mileage",
                table: "ListedVehicles");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ListedVehicles");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "ListedVehicles");
        }
    }
}
