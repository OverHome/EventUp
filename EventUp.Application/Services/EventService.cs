using EventUp.Application.Interfaces;
using EventUp.Application.Interfaces.Services;
using EventUp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventUp.Application.Services;

public class EventService : IEventService
{
    private readonly IEventDbContext _eventsDbContext;
    private readonly IStationService _stationService;
    private readonly IEventTypeService _eventTypeService;

    public EventService(IEventDbContext eventsDbContext, IStationService stationService,
        IEventTypeService eventTypeService)
    {
        _eventsDbContext = eventsDbContext;
        _stationService = stationService;
        _eventTypeService = eventTypeService;
    }

    public async Task<List<Event>> GetAllEvents()
    {
        return await _eventsDbContext.Events
            .Include(e => e.Station)
            .Include(e => e.EventType)
            .ToListAsync();
    }

    public async Task<Event> GetEventsById(int id)
    {
        return await _eventsDbContext.Events
            .Include(e => e.Station)
            .Include(e => e.EventType)
            .FirstAsync(e => e.Id == id);
    }

    public async Task<Event> AddEvent(Event newEvent)
    {
        newEvent.Station = await _stationService.GetStationById(newEvent.StationId);
        if (newEvent.Station is null)
        {
            throw new NullReferenceException();
        }

        foreach (var e in newEvent.EventTypeIds) newEvent.EventType.Add(await _eventTypeService.GetEventTypeById(e));

        var obj = await _eventsDbContext.Events.AddAsync(newEvent);
        await _eventsDbContext.SaveChangesAsync(default);
        return obj.Entity;
    }

    public async Task DeleteEventById(int id)
    {
        _eventsDbContext.Events.Remove(await _eventsDbContext.Events.FirstAsync(e => e.Id == id));
        await _eventsDbContext.SaveChangesAsync(default);
    }
}