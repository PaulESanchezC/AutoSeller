using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Update_MakerNameCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Makers",
                newName: "MakerName");

            migrationBuilder.RenameIndex(
                name: "IX_Makers_Name",
                table: "Makers",
                newName: "IX_Makers_MakerName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MakerName",
                table: "Makers",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_Makers_MakerName",
                table: "Makers",
                newName: "IX_Makers_Name");
        }
    }
}
