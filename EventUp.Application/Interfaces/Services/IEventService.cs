using EventUp.Domain.Models;

namespace EventUp.Application.Interfaces.Services;

public interface IEventService
{
    Task<List<Event>> GetAllEvents();
    Task<Event> GetEventsById(int id);
    Task<Event> AddEvent(Event newEvent);
    Task<Event> UpdateEvent(Event updateEvent);
    Task DeleteEventById(int id);
    Task<List<Event>> GetEventByFilter(EventFilter filter);

}