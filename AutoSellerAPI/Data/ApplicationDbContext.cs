using Data.FluentApis;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.ApplicationUsersModels;
using Models.ImagesModels;
using Models.IntentsModels;
using Models.ListedVehiclesModels;
using Models.MakerModels;
using Models.VehiclesModels;

namespace Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) :base (options)
    {
    }

    public DbSet<ApplicationUser> AppUsers { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Intent> Intents { get; set; }
    public DbSet<Maker> Makers { get; set; }
    public DbSet<ListedVehicle> ListedVehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ImagesFluentApi());
        builder.ApplyConfiguration(new IntentsFluentApi());
        builder.ApplyConfiguration(new MakerFluentApi());
        builder.ApplyConfiguration(new VehiclesFluentApi());
        builder.ApplyConfiguration(new ListedVehicleFluentApi());
    }
}