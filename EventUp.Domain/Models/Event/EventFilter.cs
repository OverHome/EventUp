namespace EventUp.Domain.Models;

public class EventFilter
{
    public DateTime? Date { get; set; }
    public List<int>? EventTypeId { get; set; }
    public int? StationId { get; set; }
    public string? Search { get; set; }
}