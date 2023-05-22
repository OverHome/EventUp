using System.Globalization;
using System.Reflection;
using EventUp.Infrastructure;
using Microsoft.AspNetCore.Localization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            
    //... and tell Swagger to use those XML comments.
    opt.IncludeXmlComments(xmlPath);
});
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);  
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddValidation();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US") },
    SupportedUICultures = new List<CultureInfo> { new CultureInfo("en-US") }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();