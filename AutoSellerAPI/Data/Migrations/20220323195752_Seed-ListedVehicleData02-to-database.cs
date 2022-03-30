using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class SeedListedVehicleData02todatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'02aac73b-c806-4dbc-a944-fe2b1ff697bd', N'Automatic', N'Gray', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'01c021d5-9476-47bc-bcf2-a5a25eb65b06', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-23 13:00:32', N'0001-01-01 00:00:00', 0, N'FWD', 103000, 22000, 2018, N'0001-01-01 00:00:00', 0)
                INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'191b1dcb-09c9-499d-b0c9-37069ae1636f', N'Automatic', N'Red', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'7a5b1399-2532-4fc0-b20e-9f517b04582a', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'2022-03-23 13:55:27', N'0001-01-01 00:00:00', 0, N'FWD', 193000, 20000, 2020, N'0001-01-01 00:00:00', 0)
                INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'64bf6daa-2787-494c-837c-bb256813d8f2', N'Manual', N'Red', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'a94d5d01-989b-4242-bf34-6e90abadd67c', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-23 12:57:21', N'0001-01-01 00:00:00', 0, N'FWD', 189000, 14000, 2017, N'0001-01-01 00:00:00', 0)
                INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'808dda8d-5cbd-4264-9965-4a33cc1b9bd7', N'Automatic', N'Gray', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'788f2175-4e43-48fe-8110-28d198999109', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'2022-03-23 14:07:37', N'0001-01-01 00:00:00', 0, N'FWD', 250000, 30000, 2000, N'0001-01-01 00:00:00', 0)
                INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'f4926e5f-d3f6-47f4-83a7-c2e3fe9d390f', N'Manual', N'White', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'940d43bd-655f-4c78-9f5a-b556e42922ce', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'2022-03-23 14:12:03', N'0001-01-01 00:00:00', 0, N'FWD', 150000, 25000, 2018, N'0001-01-01 00:00:00', 0)
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
