using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Create_FluentApiForModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Intents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "NEWID()",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleName",
                table: "Vehicles",
                column: "VehicleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Makers_Name",
                table: "Makers",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleName",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Makers_Name",
                table: "Makers");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Intents",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValue: "NEWID()");
        }
    }
}
