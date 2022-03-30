using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Update_IntentTbl_intentReceiverIdTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intents_AspNetUsers_IntentReciverId",
                table: "Intents");

            migrationBuilder.RenameColumn(
                name: "IntentReciverId",
                table: "Intents",
                newName: "IntentReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Intents_IntentReciverId",
                table: "Intents",
                newName: "IX_Intents_IntentReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Intents_AspNetUsers_IntentReceiverId",
                table: "Intents",
                column: "IntentReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Intents_AspNetUsers_IntentReceiverId",
                table: "Intents");

            migrationBuilder.RenameColumn(
                name: "IntentReceiverId",
                table: "Intents",
                newName: "IntentReciverId");

            migrationBuilder.RenameIndex(
                name: "IX_Intents_IntentReceiverId",
                table: "Intents",
                newName: "IX_Intents_IntentReciverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Intents_AspNetUsers_IntentReciverId",
                table: "Intents",
                column: "IntentReciverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
