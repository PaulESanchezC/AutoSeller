using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Update_IntentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intents_Vehicles_VehicleOfIntentId",
                table: "Intents");

            migrationBuilder.DropIndex(
                name: "IX_Intents_VehicleOfIntentId",
                table: "Intents");

            migrationBuilder.DropColumn(
                name: "VehicleOfIntentId",
                table: "Intents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VehicleOfIntentId",
                table: "Intents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Intents_VehicleOfIntentId",
                table: "Intents",
                column: "VehicleOfIntentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Intents_Vehicles_VehicleOfIntentId",
                table: "Intents",
                column: "VehicleOfIntentId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
