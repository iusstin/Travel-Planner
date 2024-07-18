using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtUtils, JwtUtils>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPlaceRepository, PlaceRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ITripRepository, TripRepository>();
        return services;
    }
}
