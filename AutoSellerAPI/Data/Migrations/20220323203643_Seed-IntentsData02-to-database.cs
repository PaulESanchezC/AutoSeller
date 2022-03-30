using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class SeedIntentsData02todatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'456a1985-1dea-4a8e-ac8c-54d8fdf4596d', N'2022-03-23 14:27:47', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'dce92991-de0e-4390-b83b-87f8d7047f50', N'808dda8d-5cbd-4264-9965-4a33cc1b9bd7', N'0001-01-01 00:00:00', 0, 0, 0)
                INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'64c2dbae-0a6d-41d8-889c-de0121938dda', N'2022-03-23 14:27:04', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'dce92991-de0e-4390-b83b-87f8d7047f50', N'f4926e5f-d3f6-47f4-83a7-c2e3fe9d390f', N'0001-01-01 00:00:00', 0, 0, 0)
                INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'ab98112a-f74a-4b13-9e91-49fa4e4283fb', N'2022-03-23 14:28:01', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'dce92991-de0e-4390-b83b-87f8d7047f50', N'191b1dcb-09c9-499d-b0c9-37069ae1636f', N'0001-01-01 00:00:00', 0, 0, 0)
                INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'b40a411c-92f8-4cab-aa22-116cab49c0be', N'2022-03-20 00:31:26', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'64bf6daa-2787-494c-837c-bb256813d8f2', N'2022-03-20 01:53:00', 0, 0, 0)
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
