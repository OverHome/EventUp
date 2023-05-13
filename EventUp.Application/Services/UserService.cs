using EventUp.Application.Interfaces;
using EventUp.Application.Interfaces.Services;
using EventUp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventUp.Application.Services;

public class UserService : IUserService
{
    private readonly IUserDbContext _userDbContext;
    private readonly IEventDbContext _eventDbContext;

    public UserService(IUserDbContext userDbContext, IEventDbContext eventDbContext)
    {
        _userDbContext = userDbContext;
        _eventDbContext = eventDbContext;
    }

    public async Task<User> AddUser(User newUser)
    {
        var obj = await _userDbContext.Users.AddAsync(newUser);
        await _userDbContext.SaveChangesAsync(default);
        return obj.Entity;
    }

    public async Task<User> GetUser(string name)
    {
        return (await _userDbContext.Users.FirstOrDefaultAsync(e => e.Name == name))!;
    }

    public async Task<User> GetUser(int id)
    {
        return (await _userDbContext.Users.FirstOrDefaultAsync(e => e.Id == id))!;
    }

    public async Task<Event> AddUserFavoriteEvents(int userId, int eventId)
    {
        var user = await _userDbContext.Users
            .Include(e => e.FavoriteEvents)
            .ThenInclude(e => e.EventType)
            .FirstAsync(e => e.Id == userId);
        user.FavoriteEvents.Add(await _eventDbContext.Events
            .Include(e => e.EventType)
            .Include(e => e.Station)
            .FirstAsync(e => e.Id == eventId));
        await _userDbContext.SaveChangesAsync(default);
        return user.FavoriteEvents.Last();
    }

    public async Task DeleteUserFavoriteEvents(int userId, int favoriteEventId)
    {
        var user = await _userDbContext.Users.FirstAsync(e => e.Id == userId);
        user.FavoriteEvents.Remove(await _eventDbContext.Events.FirstAsync(e => e.Id == favoriteEventId));
        await _userDbContext.SaveChangesAsync(default);
    }

    public async Task<List<Event>> GetUserFavoriteEvents(int userId)
    {
        var user = await _userDbContext.Users
            .Include(e => e.FavoriteEvents)
            .ThenInclude(e => e.Station)
            .Include(e => e.FavoriteEvents)
            .ThenInclude(e => e.EventType)
            .FirstAsync(e => e.Id == userId);
        return user.FavoriteEvents;
    }
}