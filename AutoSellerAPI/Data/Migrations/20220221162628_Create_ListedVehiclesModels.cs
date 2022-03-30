using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Create_ListedVehiclesModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListedVehicleId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ListedVehicles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Transmission = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    VehicleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListedVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListedVehicles_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListedVehicles_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ListedVehicleId",
                table: "Images",
                column: "ListedVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_ListedVehicles_ApplicationUserId",
                table: "ListedVehicles",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListedVehicles_VehicleId",
                table: "ListedVehicles",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_ListedVehicles_ListedVehicleId",
                table: "Images",
                column: "ListedVehicleId",
                principalTable: "ListedVehicles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_ListedVehicles_ListedVehicleId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "ListedVehicles");

            migrationBuilder.DropIndex(
                name: "IX_Images_ListedVehicleId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ListedVehicleId",
                table: "Images");
        }
    }
}
