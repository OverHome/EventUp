using System.Text.Json.Serialization;

namespace EventUp.Domain.Models;

public class EventType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    
    [JsonIgnore]
    public List<Event> Events { get; set; } = new List<Event>();
}