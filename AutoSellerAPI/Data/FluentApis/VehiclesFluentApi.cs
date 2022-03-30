using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.VehiclesModels;

namespace Data.FluentApis;

public class VehiclesFluentApi : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.Property(k => k.VehicleId).HasDefaultValue("NEWID()");
    }
}