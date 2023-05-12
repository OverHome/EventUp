using EventUp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventUp.Application.Interfaces;

public interface IEventsDbContext
{
    DbSet<Event> Events { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}