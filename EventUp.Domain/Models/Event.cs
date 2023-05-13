using System.Text.Json.Serialization;

namespace EventUp.Domain.Models;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public Station? Station { get; set; }
    public List<EventType> EventType { get; set; } = new List<EventType>();
    public DateTime StartDuring { get; set; }
    public DateTime EndDuring { get; set; }
    
    [JsonIgnore] public int StationId { get; set; }
    [JsonIgnore] public List<int> EventTypeIds { get; set; } = new List<int>();
    [JsonIgnore] public List<User> InFavoriteUsers { get; set; } = new List<User>();
}