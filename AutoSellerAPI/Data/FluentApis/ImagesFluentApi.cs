using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.ImagesModels;

namespace Data.FluentApis;

public class ImagesFluentApi : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.Property(k => k.ImageId).HasDefaultValue("NEWID()");
    }
}