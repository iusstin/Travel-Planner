﻿using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public class AppDbContext: IdentityDbContext<User>, IAppDbContext
{
    private readonly IConfiguration _config;
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer(_config.GetConnectionString("AppDbContextAzure"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        new TripMatesConfiguration().Configure(builder.Entity<TripMate>());
        new TripPlaceConfiuration().Configure(builder.Entity<TripPlace>());
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<TripMate> TripMates { get; set; }
    public DbSet<TripPlace> TripPlaces { get; set; }
}
