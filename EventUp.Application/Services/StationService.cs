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

    public async Task<List<Station>> AddStations(Station newStation)
    {
        await _stationDbContext.Stations.AddAsync(newStation);
        await _stationDbContext.SaveChangesAsync(default);
        return await _stationDbContext.Stations.ToListAsync(); 
    }
}