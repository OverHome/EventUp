﻿using EventUp.Application.Interfaces;
using EventUp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventUp.Infrastructure.Data;

public class AppDbContext : DbContext, IEventDbContext, IStationDbContext, IEventTypeDbContext
{
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Station> Stations => Set<Station>();
    public DbSet<EventType> EventTypes => Set<EventType>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}