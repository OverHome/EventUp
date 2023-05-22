using EventUp.Application.Interfaces;
using EventUp.Domain.Models;
using EventUp.Infrastructure.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        try
        {
            Database.Migrate(); 
        }
        catch
        {
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>(entity =>
            entity.Property(e => e.EventTypeIds)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList())
                .Metadata.SetValueComparer(new ValueComparer<List<int>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList())));
        
        modelBuilder.Entity<User>().HasData(new User()
        {
            Id = 1,
            Name = _configuration["AdminUser:Name"],
            PasswordHash = HashPasword.Hash(_configuration["AdminUser:Password"]),
            UserRoles = UserRoles.Admin
        });
    }

    
}