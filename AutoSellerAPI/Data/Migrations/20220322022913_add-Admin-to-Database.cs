using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addAdmintoDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[AspNetUsers] 
([Id], 
[FirstName], 
[LastName], 
[Address], 
[City], 
[StateOrProvince], 
[UserName], 
[NormalizedUserName], 
[Email], [NormalizedEmail], 
[EmailConfirmed], 
[PasswordHash], 
[SecurityStamp], 
[ConcurrencyStamp], 
[PhoneNumber], 
[PhoneNumberConfirmed], 
[TwoFactorEnabled], 
[LockoutEnd], 
[LockoutEnabled], 
[AccessFailedCount]) 

VALUES (
N'b583c380-7610-4160-9487-0055c0e1d786', 
N'Paulyglot', 
N'Sanchez', 
N'does not matter', 
N'does not matter', 
N'does not matter', 
N'paulesanchezc@outlook.com', 
N'PAULESANCHEZC@OUTLOOK.COM', 
N'paulesanchezc@outlook.com', 
N'PAULESANCHEZC@OUTLOOK.COM', 
1, 
N'AQAAAAEAACcQAAAAECOQB+sMz39poNXpVeOlB8mq0rq7hJq6Byj0/0y8+8MDHXYyzNujVpJqgcIznaTvfg==', 
N'CYYCEZCEO35CUWQSKM6VEBRTAXABXSRN', 
N'01c08957-79b1-4089-a896-0189a62a9b87', 
N'does not matter', 0, 0, NULL, 1, 0)
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
