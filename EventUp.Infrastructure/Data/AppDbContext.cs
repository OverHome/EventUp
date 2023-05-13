using EventUp.Application.Interfaces;
using EventUp.Domain.Models;
using EventUp.Infrastructure.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventUp.Infrastructure.Data;

public class AppDbContext : DbContext, IEventDbContext, IStationDbContext, IEventTypeDbContext, IUserDbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Station> Stations => Set<Station>();
    public DbSet<EventType> EventTypes => Set<EventType>();
    public DbSet<User> Users => Set<User>();
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasData(new User()
        {
            Id = 1,
            Name = _configuration["AdminUser:Name"],
            PasswordHash = HashPasword.Hash(_configuration["AdminUser:Password"]),
            UserRoles = UserRoles.Admin
        });
    }

    
}