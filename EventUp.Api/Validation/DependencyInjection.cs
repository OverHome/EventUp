using System.Text;
using EventUp.Api.Validation.Event;
using EventUp.Api.Validation.EventType;
using EventUp.Api.Validation.Station;
using EventUp.Api.Validation.User;
using EventUp.Application.Dto.Event;
using EventUp.Application.Dto.EventType;
using EventUp.Application.Dto.Station;
using EventUp.Application.Dto.User;
using EventUp.Application.Interfaces.Services;
using EventUp.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EventUp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
       
        services.AddFluentValidationAutoValidation(c =>{c.DisableDataAnnotationsValidation = true;});

        services.AddScoped<IValidator<AddEventDto>, AddEventValid>();
        services.AddScoped<IValidator<UpdateEventDto>, UpdateEventValid>();
        services.AddScoped<IValidator<AddEventTypeDto>, AddEventTypeValid>();
        services.AddScoped<IValidator<AddStationDto>, AddStationValid>();
        services.AddScoped<IValidator<RegisterUserDto>, AddUserValid>();
        services.AddScoped<IValidator<LoginUserDto>, LoginUserValid>();


        return services;
    }
}