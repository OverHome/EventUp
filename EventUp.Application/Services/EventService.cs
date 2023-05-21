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
            throw new Exception();
        }

        foreach (var e in newEvent.EventTypeIds) newEvent.EventType.Add(await _eventTypeService.GetEventTypeById(e));

        var obj = await _eventsDbContext.Events.AddAsync(newEvent);
        await _eventsDbContext.SaveChangesAsync(default);
        return obj.Entity;
    }

    public async Task<Event> UpdateEvent(Event updateEvent)
    {
        updateEvent.Station = await _stationService.GetStationById(updateEvent.StationId);
        if (updateEvent.Station is null)
        {
            throw new Exception();
        }
        _eventsDbContext.Events.Update(updateEvent);
        var @event = await GetEventsById(updateEvent.Id);
        @event.EventType = new List<EventType>();
        foreach (var e in @event.EventTypeIds) @event.EventType.Add(await _eventTypeService.GetEventTypeById(e));
        await _eventsDbContext.SaveChangesAsync(default);
        return @event;
    }

    public async Task DeleteEventById(int id)
    {
        _eventsDbContext.Events.Remove(await _eventsDbContext.Events.FirstAsync(e => e.Id == id));
        await _eventsDbContext.SaveChangesAsync(default);
    }

    public async Task<List<Event>> GetEventByFilter(EventFilter filter)
    {
        var allEvent =_eventsDbContext.Events
            .Include(e => e.Station)
            .Include(e => e.EventType)
            .AsQueryable();
        
        if (filter.Date != null)
        {
            allEvent = allEvent.Where(e => e.StartDuring.Date == filter.Date.Value.Date);
        }
        if (filter.StationId != null)
        {
            allEvent = allEvent.Where(e => e.StationId == filter.StationId);
        }
        if (filter.EventTypeId != null && filter.EventTypeId.Any())
        {
            allEvent = allEvent.Where(e => filter.EventTypeId.All(id => e.EventTypeIds.Contains(id)));
        }
        if (filter.Search != null)
        {
           allEvent = allEvent.Where(e => e.Title.Contains(filter.Search) || e.About.Contains(filter.Search));
        }

        return await allEvent.ToListAsync();
    }

    public async Task<List<Event>> GetActiveEvent()
    {
        return await _eventsDbContext.Events
            .Include(e => e.Station)
            .Include(e => e.EventType)
            .Where(e => e.EndDuring > DateTime.Now)
            .ToListAsync();    }
}