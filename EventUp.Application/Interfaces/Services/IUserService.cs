using EventUp.Domain.Models;

namespace EventUp.Application.Interfaces.Services;

public interface IUserService
{
    Task<User> AddUser(User newUser);
    Task<User> GetUser(string name);
    Task<User> GetUser(int id);
    Task<Event> AddUserFavoriteEvents(int userId, int eventId);
    Task DeleteUserFavoriteEvents(int userId, int favoriteEventId);
    Task<List<Event>> GetUserFavoriteEvents(int userId);
}