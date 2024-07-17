using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class TripMatesConfiguration : IEntityTypeConfiguration<TripMate>
{
    public void Configure(EntityTypeBuilder<TripMate> builder)
    {
        builder.HasKey(m => new { m.TripId, m.UserId });
    }
}
