using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.MakerModels;

namespace Data.FluentApis;

public class MakerFluentApi : IEntityTypeConfiguration<Maker>
{
    public void Configure(EntityTypeBuilder<Maker> builder)
    {
        builder.Property(k => k.MakerId).HasDefaultValue("NEWID()");
    }
}