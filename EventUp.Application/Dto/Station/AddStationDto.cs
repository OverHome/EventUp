namespace EventUp.Application.Dto.Station;

public class AddStationDto
{
    public string PlaceName { get; set; } = string.Empty;
    public string PlaceAddress { get; set; } = string.Empty;
    public double GeoLong { get; set; }
    public double GeoLat { get; set; }
}