using EventUp.Application.Interfaces;
using EventUp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventUp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IEventDbContext>(provider => provider.GetService<AppDbContext>());
        services.AddScoped<IStationDbContext>(provider => provider.GetService<AppDbContext>());
        services.AddScoped<IEventTypeDbContext>(provider => provider.GetService<AppDbContext>());
        services.AddScoped<IUserDbContext>(provider => provider.GetService<AppDbContext>());
        return services;
    }
}