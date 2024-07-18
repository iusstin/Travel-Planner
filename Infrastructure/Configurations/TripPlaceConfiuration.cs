using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class TripPlaceConfiuration : IEntityTypeConfiguration<TripPlace>
{
    public void Configure(EntityTypeBuilder<TripPlace> builder)
    {
        builder.HasKey(m => new { m.TripId, m.PlaceId });
    }
}
