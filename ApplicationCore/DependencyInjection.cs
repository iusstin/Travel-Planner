using ApplicationCore.Behaviours;
using ApplicationCore.Data;
using FluentValidation;
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
            config.AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>));
            config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
