using EventUp.Domain.Models;

namespace EventUp.Application.Interfaces.Services;

public interface IStationService
{
    Task<List<Station>> GetAllStations();
    Task<Station> GetStationById(int id);
    Task<Station> AddStations(Station newStation);
    Task DeleteStationsById(int id);
}