using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddGuestRolestoDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
                VALUES (N'9551516f-99d9-434d-9242-0ea524d76d2d', N'Guest', N'GUEST', N'd2275a1e-ad7d-47d6-9d7a-76e1f5a8ffba')"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
