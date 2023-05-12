using EventUp.Application.Interfaces;
using EventUp.Application.Interfaces.Services;
using EventUp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EventUp.Application.Services;

public class StationService : IStationService
{
    private readonly IStationDbContext _stationDbContext;

    public StationService(IStationDbContext stationDbContext)
    {
        _stationDbContext = stationDbContext;
    }
    
    public async Task<List<Station>> GetAllStations()
    {
        return await _stationDbContext.Stations.ToListAsync(); 
    }
    
    public async Task<Station> GetStationById(int id)
    {
        return (await _stationDbContext.Stations.FirstOrDefaultAsync(s => s.Id == id))!;
    }

    public async Task<Station> AddStations(Station newStation)
    {
        var obj = await _stationDbContext.Stations.AddAsync(newStation);
        await _stationDbContext.SaveChangesAsync(default);
        return obj.Entity; 
    }

    public async Task DeleteStationsById(int id)
    {
        _stationDbContext.Stations.Remove((await _stationDbContext.Stations.FirstAsync(s => s.Id == id))!);
        await _stationDbContext.SaveChangesAsync(default);
    }
}