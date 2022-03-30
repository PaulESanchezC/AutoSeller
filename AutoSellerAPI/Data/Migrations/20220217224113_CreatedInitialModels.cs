using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class CreatedInitialModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "MakerId",
                table: "Vehicles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VehicleName",
                table: "Vehicles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfIntent",
                table: "Intents",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IntentReciverId",
                table: "Intents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IntentSenderId",
                table: "Intents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VehicleOfIntentId",
                table: "Intents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBytes",
                table: "Images",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "VehicleId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StateOrProvince",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Makers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_MakerId",
                table: "Vehicles",
                column: "MakerId");

            migrationBuilder.CreateIndex(
                name: "IX_Intents_IntentReciverId",
                table: "Intents",
                column: "IntentReciverId");

            migrationBuilder.CreateIndex(
                name: "IX_Intents_IntentSenderId",
                table: "Intents",
                column: "IntentSenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Intents_VehicleOfIntentId",
                table: "Intents",
                column: "VehicleOfIntentId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Intents_AspNetUsers_IntentReciverId",
                table: "Intents",
                column: "IntentReciverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Intents_AspNetUsers_IntentSenderId",
                table: "Intents",
                column: "IntentSenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Intents_Vehicles_VehicleOfIntentId",
                table: "Intents",
                column: "VehicleOfIntentId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Makers_MakerId",
                table: "Vehicles",
                column: "MakerId",
                principalTable: "Makers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Vehicles_VehicleId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Intents_AspNetUsers_IntentReciverId",
                table: "Intents");

            migrationBuilder.DropForeignKey(
                name: "FK_Intents_AspNetUsers_IntentSenderId",
                table: "Intents");

            migrationBuilder.DropForeignKey(
                name: "FK_Intents_Vehicles_VehicleOfIntentId",
                table: "Intents");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Makers_MakerId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Makers");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_MakerId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Intents_IntentReciverId",
                table: "Intents");

            migrationBuilder.DropIndex(
                name: "IX_Intents_IntentSenderId",
                table: "Intents");

            migrationBuilder.DropIndex(
                name: "IX_Intents_VehicleOfIntentId",
                table: "Intents");

            migrationBuilder.DropIndex(
                name: "IX_Images_VehicleId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "MakerId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleName",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "DateOfIntent",
                table: "Intents");

            migrationBuilder.DropColumn(
                name: "IntentReciverId",
                table: "Intents");

            migrationBuilder.DropColumn(
                name: "IntentSenderId",
                table: "Intents");

            migrationBuilder.DropColumn(
                name: "VehicleOfIntentId",
                table: "Intents");

            migrationBuilder.DropColumn(
                name: "ImageBytes",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "StateOrProvince",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
