using System.Text.Json.Serialization;

namespace EventUp.Domain.Models;

public class Station
{
    public int Id { get; set; }
    public string PlaceName { get; set; } = string.Empty;
    public string PlaceAddress { get; set; } = string.Empty;
    public double GeoLong { get; set; }
    public double GeoLat { get; set; }
    [JsonIgnore]
    public List<Event>? Events { get; set; }
}