using System.Text;
using EventUp.Application.Interfaces.Services;
using EventUp.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EventUp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IStationService, StationService>();
        services.AddScoped<IEventTypeService, EventTypeService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        
            
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["JwtAuthentication:Issuer"],
                ValidateAudience = true,
                ValidAudience = configuration["JwtAuthentication:Audience"],
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes(configuration["JwtAuthentication:Key"])),
                ValidateIssuerSigningKey = true,
            };
        });
        services.AddAuthorization();
    
        return services;
    }
}