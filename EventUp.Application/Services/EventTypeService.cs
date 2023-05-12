using EventUp.Application.Interfaces;
using EventUp.Application.Interfaces.Services;
using EventUp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventUp.Application.Services;

public class EventTypeService : IEventTypeService
{
    private readonly IEventTypeDbContext _eventTypeDbContext;

    public EventTypeService(IEventTypeDbContext eventTypeDbContext)
    {
        _eventTypeDbContext = eventTypeDbContext;
    }

    public async Task<List<EventType>> GetAllEventType()
    {
        return await _eventTypeDbContext.EventTypes.ToListAsync();
    }

    public async Task<EventType> GetEventTypeById(int id)
    {
        return await _eventTypeDbContext.EventTypes.FirstAsync(e => e.Id == id);
    }

    public async Task<EventType> AddEventType(EventType newEvent)
    {
        var obj = await _eventTypeDbContext.EventTypes.AddAsync(newEvent);
        await _eventTypeDbContext.SaveChangesAsync(default);
        return obj.Entity;
    }

    public async Task DeleteEventTypeById(int id)
    {
        _eventTypeDbContext.EventTypes.Remove(await _eventTypeDbContext.EventTypes.FirstAsync(e => e.Id == id));
        await _eventTypeDbContext.SaveChangesAsync(default);
    }
}