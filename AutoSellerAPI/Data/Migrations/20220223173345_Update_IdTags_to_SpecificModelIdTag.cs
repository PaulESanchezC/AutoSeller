using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Update_IdTags_to_SpecificModelIdTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Vehicles_VehicleId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_VehicleId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Vehicles",
                newName: "VehicleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Makers",
                newName: "MakerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ListedVehicles",
                newName: "ListedVehicleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Intents",
                newName: "IntentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Images",
                newName: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "Vehicles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MakerId",
                table: "Makers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ListedVehicleId",
                table: "ListedVehicles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IntentId",
                table: "Intents",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Images",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "VehicleId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_VehicleId",
                table: "Images",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Vehicles_VehicleId",
                table: "Images",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }
    }
}
