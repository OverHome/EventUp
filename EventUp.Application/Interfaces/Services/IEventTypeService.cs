using EventUp.Domain.Models;

namespace EventUp.Application.Interfaces.Services;

public interface IEventTypeService
{
    Task<List<EventType>> GetAllEventType();
    Task<EventType> GetEventTypeById(int id);
    Task<EventType> AddEventType(EventType newEvent);
    Task DeleteEventTypeById(int id);
}