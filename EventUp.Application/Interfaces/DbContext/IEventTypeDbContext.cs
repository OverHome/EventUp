using EventUp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventUp.Application.Interfaces;

public interface IEventTypeDbContext
{
    DbSet<EventType> EventTypes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}