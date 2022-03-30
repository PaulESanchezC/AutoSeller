using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.IntentsModels;

namespace Data.FluentApis;

public class IntentsFluentApi : IEntityTypeConfiguration<Intent>
{
    public void Configure(EntityTypeBuilder<Intent> builder)
    {
        builder.HasOne(i => i.IntentSender)
            .WithMany(a => a.IntentsSent)
            .HasForeignKey(k => k.IntentSenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.IntentReceiver)
            .WithMany(a => a.IntentsRecieved)
            .HasForeignKey(k => k.IntentReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(k => k.IntentId).HasDefaultValue("NEWID()");
    }
}