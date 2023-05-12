using EventUp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventUp.Application.Interfaces;

public interface IStationDbContext
{
    DbSet<Station> Stations { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}