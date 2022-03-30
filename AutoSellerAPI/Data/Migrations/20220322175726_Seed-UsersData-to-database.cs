using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class SeedUsersDatatodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Address], [City], [StateOrProvince], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'Tutorial User # 2', N'Tutorial User # 2', N'Tutorial User # 2', N'Tutorial User # 2', N'Tutorial User # 2', N'User2@tutorial.com', N'USER2@TUTORIAL.COM', N'User2@tutorial.com', N'USER2@TUTORIAL.COM', 1, N'AQAAAAEAACcQAAAAEK1X5LuURVIAQMaaOEmEqFM2mLPgESIlox1fpKdukkQGeZ3s7X+oW3BwimfthRfm+Q==', N'GGPGF4FAXMNTGDXKSWI4JQQFWLKYZUHJ', N'99bcc594-2d76-47b9-84c1-93c985568824', N'Tutorial User # 2', 0, 0, NULL, 1, 0)
            INSERT INTO [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Address], [City], [StateOrProvince], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'dce92991-de0e-4390-b83b-87f8d7047f50', N'Tutorial User # 3', N'Tutorial User # 3', N'Tutorial User # 3', N'Tutorial User # 3', N'Tutorial User # 3', N'User3@tutorial.com', N'USER3@TUTORIAL.COM', N'User3@tutorial.com', N'USER3@TUTORIAL.COM', 1, N'AQAAAAEAACcQAAAAEMppqMXZiChQaJjEB3VALD4O9YD8Nk6Hr8BELegeUq5GM5lh+1hOABH/udTiXLFEvg==', N'GIGD42FEBITJARCM2XNZZNF7NM2FVSWP', N'a8bc3406-0592-4966-9157-4e40988203da', N'Tutorial User # 3', 0, 0, NULL, 1, 0)
            INSERT INTO [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Address], [City], [StateOrProvince], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'Tutorial User # 1', N'Tutorial User # 1', N'Tutorial User # 1', N'Tutorial User # 1', N'Tutorial User # 1', N'user1@tutorial.com', N'USER1@TUTORIAL.COM', N'user1@tutorial.com', N'USER1@TUTORIAL.COM', 1, N'AQAAAAEAACcQAAAAEODyGWyEhhnwgd4GxUNm+CmxjkOqEdDNHGae1xyhmjW9rV5jFmtcc6N1MFYjs44MfA==', N'ZJXUCOBVQGFQSR7WHJH6AXWAQR26BQ2R', N'da6fb2b0-68bd-4ea4-9847-b0cb7d4234a1', N'Tutorial User # 1', 0, 0, N'3/18/2022 3:33:01 AM +00:00', 1, 0)
            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
