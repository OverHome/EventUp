using EventUp.Application.Interfaces;
using EventUp.Application.Interfaces.Services;
using EventUp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventUp.Application.Services;

public class EventService : IEventService
{
    private readonly IEventsDbContext _eventsDbContext;
    private readonly IStationService _stationService;

    public EventService(IEventsDbContext eventsDbContext, IStationService stationService)
    {
        _eventsDbContext = eventsDbContext;
        _stationService = stationService;
    }
    
    public async Task<List<Event>> GetAllEvents()
    {
        return await _eventsDbContext.Events.ToListAsync();
    }

    public async Task<Event> GetEventsById(int id)
    {
        return (await _eventsDbContext.Events.FirstOrDefaultAsync(e => e.Id == id))!;
    }

    public async Task<List<Event>> AddEvent(Event newEvent)
    {
        newEvent.Station = await _stationService.GetStationById(newEvent.StationId);
        if (newEvent.Station is null)
        {
            throw new NullReferenceException();
        }
        await _eventsDbContext.Events.AddAsync(newEvent);
        await _eventsDbContext.SaveChangesAsync(default);
        return await _eventsDbContext.Events.ToListAsync(); 
    }

    public async Task<List<Event>> DeleteEventById(int id)
    {
        _eventsDbContext.Events.Remove((await _eventsDbContext.Events.FirstOrDefaultAsync(e => e.Id == id))!);
        await _eventsDbContext.SaveChangesAsync(default);
        return await _eventsDbContext.Events.ToListAsync();
    }
}