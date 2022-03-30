using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.ListedVehiclesModels;

namespace Data.FluentApis;

public class ListedVehicleFluentApi : IEntityTypeConfiguration<ListedVehicle>
{
    public void Configure(EntityTypeBuilder<ListedVehicle> builder)
    {
        builder.Property(k => k.ListedVehicleId).HasDefaultValue("NEWID()");
    }
}