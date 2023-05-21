using System.Text.Json.Serialization;

namespace EventUp.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRoles UserRoles { get; set; }
    [JsonIgnore]
    public List<Event> FavoriteEvents { get; set; } = new List<Event>();
}