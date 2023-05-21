namespace EventUp.Application.Dto.Event;

public class UpdateEventDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? About { get; set; } 
    public int? StationId { get; set; }
    public List<int>? EventTypeIds { get; set; }
    public DateTime? StartDuring { get; set; }
    public DateTime? EndDuring { get; set; }
}