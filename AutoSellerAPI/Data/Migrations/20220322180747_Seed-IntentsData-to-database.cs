using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class SeedIntentsDatatodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'181546d9-dee8-4f35-81cb-7cd79ee20a2c', N'2022-03-20 02:08:38', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'1f9a69b7-e585-4103-a62e-854c70e4fe6d', N'0001-01-01 00:00:00', 0, 0, 0)
INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'2869c41d-856a-4f05-b4a0-35fcdea361a6', N'2022-03-20 02:08:32', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'e4d6feae-953d-4faf-9e46-b85940df990f', N'2022-03-20 02:12:24', 0, 0, 0)
INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'7fdaac96-659c-4a9b-9ffc-c4372a9c4622', N'2022-03-20 00:37:02', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'6f3042f2-0ce8-4034-9a93-136943f58e03', N'2022-03-20 02:00:55', 0, 0, 0)
INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'827c7f78-0f3f-49fc-8f96-bdcf1e4db500', N'2022-03-20 02:08:25', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'f8cf56be-e51e-4c63-a4d8-6373e8b819f8', N'2022-03-20 02:10:36', 0, 0, 0)
INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'b40a411c-92f8-4cab-aa22-116cab49c0be', N'2022-03-20 00:31:26', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'64bf6daa-2787-494c-837c-bb256813d8f2', N'2022-03-20 01:53:00', 0, 0, 1)
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
