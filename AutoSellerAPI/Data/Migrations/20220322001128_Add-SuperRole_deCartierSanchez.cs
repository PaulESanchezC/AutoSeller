using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddSuperRole_deCartierSanchez : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
            VALUES (N'eaea6f97-c61b-45c4-b76f-e8af8d1a7cfc', N'deCartierSanchez', N'DECARTIERSANCHEZ', N'85e84651-ac93-4207-b910-d4142c16a164')"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
