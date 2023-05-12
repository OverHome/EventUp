using EventUp.Application.Interfaces.Services;
using EventUp.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventUp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IStationService, StationService>();
        return services;
    }
}