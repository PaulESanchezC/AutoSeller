using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class SeedMakersDatatodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'BUICK')
                INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'CHEVROLET')
                INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'9f53bca0-7626-4268-8dc6-aef96508667c', N'VOLKSWAGEN')
                INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'FORD')
                INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'2e4bf369-7a7d-477b-b4ec-32fe7dd1d78b', N'HONDA')
                INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'fa4c00ca-1de5-4118-b306-35f88d970dca', N'MAZDA')
                INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'1ee37317-c4b9-4728-b009-5be115c648fa', N'MERCEDES')
                INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'44abd3f2-eb74-4c55-8d64-53896be97133', N'MITSUBISHI')
                INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'42534b91-b666-4ac7-9985-11c94f49a10c', N'TESLA')
                INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'a0cdc54c-2132-4c2d-b2df-396111ab86c1', N'TOYOTA')
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
