using ApplicationCore.Behaviours;
using ApplicationCore.Data;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationCore;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationCore(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
