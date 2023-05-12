using EventUp.Domain.Models;
using EventUp.Infrastructure.Dto;

namespace EventUp.Application.Interfaces.Services;

public interface IEventService
{
    Task<List<Event>> GetAllEvents();
    Task<Event> GetEventsById(int id);
    Task<Event> AddEvent(Event newEvent);
    Task DeleteEventById(int id);
}