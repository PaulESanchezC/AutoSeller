using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Update_IntentModel_Add_ListedVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListedVehicleId",
                table: "Intents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Intents_ListedVehicleId",
                table: "Intents",
                column: "ListedVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Intents_ListedVehicles_ListedVehicleId",
                table: "Intents",
                column: "ListedVehicleId",
                principalTable: "ListedVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intents_ListedVehicles_ListedVehicleId",
                table: "Intents");

            migrationBuilder.DropIndex(
                name: "IX_Intents_ListedVehicleId",
                table: "Intents");

            migrationBuilder.DropColumn(
                name: "ListedVehicleId",
                table: "Intents");
        }
    }
}
